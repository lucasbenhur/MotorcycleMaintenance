using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using MotorcycleService.Application.Commands;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Responses;

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
                _logger.LogInformation("Evento Cadastrar Moto inicidado para o Id {Id}.", context.Message.Id);

                if (context.Message.Year == 2024)
                {
                    var createMotorcycleCommand = MotorcycleMapper.Mapper.Map<CreateMotorcycleCommand>(context.Message);
                    await _mediator.Send<MotorcycleResponse>(createMotorcycleCommand);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao notificar Cadastar Moto para o Id {Id}.", context.Message.Id);
            }
            finally
            {
                _logger.LogInformation("Evento Cadastrar Moto completado para o Id {Id}", context.Message.Id);
            }
        }
    }
}
