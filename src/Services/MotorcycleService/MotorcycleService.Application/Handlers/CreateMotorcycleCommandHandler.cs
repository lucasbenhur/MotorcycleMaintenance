using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Entities;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Core.Specs;
using Shared.Extensions;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, MotorcycleResponse>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<CreateMotorcycleCommandHandler> _logger;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            ILogger<CreateMotorcycleCommandHandler> logger,
            IServiceContext serviceContext,
            IMediator mediator,
            IPublishEndpoint publishEndpoint)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _serviceContext = serviceContext;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<MotorcycleResponse> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return null;

                var motorcycleEntity = MotorcycleMapper.Mapper.Map<Motorcycle>(request);
                var newMotorcycle = await _motorcycleRepository.CreateAsync(motorcycleEntity);

                _logger.LogInformation("Moto Id {Id} cadastrada", newMotorcycle.Id);

                var createMotorcycleEventMsg = MotorcycleMapper.Mapper.Map<CreateMotorcycleEvent>(newMotorcycle);
                await _publishEndpoint.Publish(createMotorcycleEventMsg);

                return MotorcycleMapper.Mapper.Map<MotorcycleResponse>(newMotorcycle);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao cadastrar a Moto Id {request.Id}. Detalhes: {ex.Message}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }

        private async Task<bool> IsValidAsync(CreateMotorcycleCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo Identificador é obrigatório");
            else if (await ExistsIdAsync(request.Id))
                _serviceContext.AddNotification($"O Identificador {request.Id} já existe");

            if (!request.Year.HasValue)
                _serviceContext.AddNotification("O campo Ano é obrigatório");
            else if (request.Year.GetValueOrDefault() <= 0)
                _serviceContext.AddNotification("Informe um Ano válido!");

            if (string.IsNullOrWhiteSpace(request.Model))
                _serviceContext.AddNotification("O campo Modelo é obrigatório");

            if (string.IsNullOrWhiteSpace(request.Plate))
                _serviceContext.AddNotification("O campo Placa é obrigatório");
            else if (!request.Plate.IsPlate())
                _serviceContext.AddNotification("Informe uma Placa válida!");
            else if (await ExistsPlateAsync(request.Plate))
                _serviceContext.AddNotification($"A Placa {request.Plate} já existe");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistsPlateAsync(string plate)
        {
            var specParams = new GetAllMotorcycleSpecParams(plate);
            var query = new GetAllMotorcyclesQuery(specParams);
            return (await _mediator.Send<ICollection<MotorcycleResponse>>(query)).Any();
        }

        private async Task<bool> ExistsIdAsync(string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            return await _mediator.Send<MotorcycleResponse>(query) is not null;
        }
    }
}
