using MassTransit;
using MediatR;
using MotorcycleService.Application.Commands;
using MotorcycleService.Core.Integrations;
using MotorcycleService.Core.Repositories;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand, bool>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRentApi _rentApi;

        public DeleteMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            IAppLogger logger,
            IServiceContext serviceContext,
            IMediator mediator,
            IPublishEndpoint publishEndpoint,
            IRentApi rentApi)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _serviceContext = serviceContext;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _rentApi = rentApi;
        }

        public async Task<bool> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleRepository.GetAsync(request.Id);

                if (motorcycle is null)
                {
                    _serviceContext.AddNotification($"A moto_id {request.Id} não existe");
                    return false;
                }

                if (await ExistRentAsync(request.Id))
                {
                    _serviceContext.AddNotification($"Existe registro de locação para a moto_id {request.Id}");
                    return false;
                }

                if (!await _motorcycleRepository.DeleteAsync(request.Id))
                {
                    _serviceContext.AddNotification($"Não foi possível remover a Moto Id {request.Id}");
                    return false;
                }

                _logger.LogInformation($"Moto Id {motorcycle.Id} removida");
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao remover a Moto Id {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        public async Task<bool> ExistRentAsync(string motorcycleId) =>
            !string.IsNullOrEmpty((await _rentApi.GetByMotorcycleIdAsync(motorcycleId!))?.Id);
    }
}
