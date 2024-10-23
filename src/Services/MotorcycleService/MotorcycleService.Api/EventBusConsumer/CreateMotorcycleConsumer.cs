using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Shared.AppLog.Services;
using Shared.Notifications.Command;

namespace MotorcycleService.Api.EventBusConsumer
{
    public class CreateMotorcycleConsumer : IConsumer<CreateMotorcycleEvent>
    {
        private readonly IAppLogger _logger;
        private readonly IMediator _mediator;

        public CreateMotorcycleConsumer(
            IAppLogger logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateMotorcycleEvent> context)
        {
            try
            {
                _logger.LogInformation($"Consumidor do Evento Cadastrar Moto inicidado para o ID {context.Message.Id}");

                if (context.Message.Year == 2024)
                {
                    var createNotificationCommand = new CreateNotificationCommand($"Foi cadastrada uma moto Ano 2024 com o ID {context.Message.Id}");
                    await _mediator.Send(createNotificationCommand);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro no consumidor do Evento Cadastar Moto para o ID {context.Message.Id}");
            }
            finally
            {
                _logger.LogInformation($"Consumidor do Evento Cadastrar Moto finalizado para o ID {context.Message.Id}");
            }
        }
    }
}
