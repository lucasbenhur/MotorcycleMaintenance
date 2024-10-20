using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Responses;
using Shared.Extensions;

namespace MotorcycleService.Application.Handlers
{
    public class PublishEventCreateMotorcycleCommandHandler : IRequestHandler<PublishEventCreateMotorcycleCommand, PublishEventeCreateMotorcycleResponse>
    {
        private readonly ILogger<PublishEventCreateMotorcycleCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        private string? Notification { get; set; }

        public PublishEventCreateMotorcycleCommandHandler(
            ILogger<PublishEventCreateMotorcycleCommandHandler> logger,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<PublishEventeCreateMotorcycleResponse> Handle(PublishEventCreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsValid(request))
                    return new PublishEventeCreateMotorcycleResponse
                    {
                        Success = false,
                        Notification = Notification
                    };

                var createMotorcycleEventMsg = MotorcycleMapper.Mapper.Map<CreateMotorcycleEvent>(request);
                await _publishEndpoint.Publish(createMotorcycleEventMsg);
                _logger.LogInformation("Publicado o Evento Cadastrar Moto para o Id {Id}.", request.Id);

                return new PublishEventeCreateMotorcycleResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                var msg = string.Format("Ocorreu um erro ao publicar o Evento Cadastrar Moto para o Id {Id}. Detalhes: {Message}.", request.Id, ex.Message);

                _logger.LogError(ex, msg);

                return new PublishEventeCreateMotorcycleResponse
                {
                    Success = false,
                    Notification = msg
                };
            }
        }

        private bool IsValid(PublishEventCreateMotorcycleCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
                Notification = "O campo Identificador é obrigatório";

            if (request.Year <= 0)
                Notification = "O campo Ano é obrigatório";

            if (string.IsNullOrWhiteSpace(request.Model))
                Notification = "O campo Modelo é obrigatório";

            if (string.IsNullOrWhiteSpace(request.Plate))
                Notification = "O campo Placa é obrigatório";

            if (!request.Plate.IsPlate())
                Notification = "Informe uma Placa válida!";

            // TODO: Validar se placa já existe (criar cmd consulta por placa)
            // TODO: Validar se id já existe (criar cmd consulta por id)

            return string.IsNullOrWhiteSpace(Notification);
        }
    }
}
