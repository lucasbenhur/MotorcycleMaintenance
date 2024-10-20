using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using Shared.Extensions;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class PublishEventCreateMotorcycleCommandHandler : IRequestHandler<PublishEventCreateMotorcycleCommand, bool>
    {
        private readonly ILogger<PublishEventCreateMotorcycleCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;

        public PublishEventCreateMotorcycleCommandHandler(
            ILogger<PublishEventCreateMotorcycleCommandHandler> logger,
            IPublishEndpoint publishEndpoint,
            IServiceContext serviceContext,
            IMediator mediator)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _serviceContext = serviceContext;
            _mediator = mediator;
        }

        public async Task<bool> Handle(PublishEventCreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await IsValidAsync(request))
                    return false;

                var createMotorcycleEventMsg = MotorcycleMapper.Mapper.Map<CreateMotorcycleEvent>(request);
                await _publishEndpoint.Publish(createMotorcycleEventMsg);
                _logger.LogInformation("Publicado o Evento Cadastrar Moto para o Id {Id}.", request.Id);

                return true;
            }
            catch (Exception ex)
            {
                var msg = string.Format("Ocorreu um erro ao publicar o Evento Cadastrar Moto para o Id {Id}. Detalhes: {Message}.", request.Id, ex.Message);
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(PublishEventCreateMotorcycleCommand request)
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
            var query = new GetMotorcycleByPlateQuery(plate);
            return await _mediator.Send<MotorcycleResponse>(query) is not null;
        }

        private async Task<bool> ExistsIdAsync(string id)
        {
            var query = new GetMotorcycleByIdQuery(id);
            return await _mediator.Send<MotorcycleResponse>(query) is not null;
        }
    }
}
