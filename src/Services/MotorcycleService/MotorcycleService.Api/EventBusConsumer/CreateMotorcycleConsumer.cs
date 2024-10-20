using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Shared.Notifications.Command;
using Shared.Notifications.Responses;

namespace MotorcycleService.Api.EventBusConsumer
{
    public class CreateMotorcycleConsumer : IConsumer<CreateMotorcycleEvent>
    {
        private readonly ILogger<CreateMotorcycleConsumer> _logger;
        private readonly IMediator _mediator;

        public CreateMotorcycleConsumer(
            ILogger<CreateMotorcycleConsumer> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateMotorcycleEvent> context)
        {
            try
            {
                _logger.LogInformation("Consumidor do Evento Cadastrar Moto inicidado para o Id {Id}", context.Message.Id);

                if (context.Message.Year == 2024)
                {
                    var msg = $"Foi cadastrada uma moto Ano 2024 com o Id {context.Message.Id}";
                    _logger.LogInformation(msg);

                    var createNotificationCommand = new CreateNotificationCommand(msg);
                    await _mediator.Send<NotificationResponse>(createNotificationCommand);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao consumir Evento Cadastar Moto para o Id {Id}", context.Message.Id);
            }
            finally
            {
                _logger.LogInformation("Consumidor do Evento Cadastrar Moto finalizado para o Id {Id}", context.Message.Id);
            }
        }
    }
}
