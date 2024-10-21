using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;
using MotorcycleService.Core.Specs;
using Shared.Extensions;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleCommand, bool>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<CreateMotorcycleCommandHandler> _logger;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateMotorcycleCommandHandler(
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

        public async Task<bool> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var motorcycle = await _motorcycleRepository.GetAsync(request.Id);

                if (motorcycle is null)
                {
                    _serviceContext.AddNotification($"Moto Id {request.Id} não encontrada");
                    return false;
                }

                motorcycle.UpdatePlate(request.Plate);

                if (!await _motorcycleRepository.UpdateAsync(motorcycle))
                {
                    _serviceContext.AddNotification($"Não foi possível atualizar a Moto Id {request.Id}");
                    return false;
                }

                _logger.LogInformation("Moto Id {Id} atualizada", motorcycle.Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao atualizar a Moto Id {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(UpdateMotorcycleCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                _serviceContext.AddNotification("O campo Identificador é obrigatório");

            if (string.IsNullOrWhiteSpace(request.Plate))
                _serviceContext.AddNotification("O campo Placa é obrigatório");
            else if (!request.Plate.IsPlate())
                _serviceContext.AddNotification("Informe uma Placa válida!");
            else if (await ExistsPlateAsync(request))
                _serviceContext.AddNotification($"A Placa {request.Plate} já existe");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistsPlateAsync(UpdateMotorcycleCommand request)
        {
            var specParams = new GetAllMotorcyclesSpecParams(request.Plate);
            var query = new GetAllMotorcyclesQuery(specParams);
            return (await _mediator.Send<ICollection<MotorcycleResponse>>(query)).Any(x => x.Id.ToUpper() != request.Id.ToUpper());
        }
    }
}
