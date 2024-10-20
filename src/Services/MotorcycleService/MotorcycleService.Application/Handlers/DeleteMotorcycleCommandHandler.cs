using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Commands;
using MotorcycleService.Core.Repositories;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand, bool>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<CreateMotorcycleCommandHandler> _logger;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteMotorcycleCommandHandler(
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

        public async Task<bool> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Validar se existe alguma locação ativa para o Id informado.

                var motorcycle = await _motorcycleRepository.GetAsync(request.Id);

                if (motorcycle is null)
                {
                    _serviceContext.AddNotification($"Moto Id {request.Id} não encontrada");
                    return false;
                }

                if (!await _motorcycleRepository.DeleteAsync(request.Id))
                {
                    _serviceContext.AddNotification($"Não foi possível remover a Moto Id {request.Id}");
                    return false;
                }

                _logger.LogInformation("Moto Id {Id} removida", motorcycle.Id);
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao remover a Moto Id {request.Id}. Detalhes: {ex.Message}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }
    }
}
