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
                if (!await IsValidAsync(request))
                    return false;

                if (!await _motorcycleRepository.DeleteAsync(request.Id))
                {
                    _serviceContext.AddNotification($"Não foi possível remover a Moto ID {request.Id}");
                    return false;
                }

                _logger.LogInformation($"Moto ID {request.Id} removida");
                return true;
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao remover a Moto ID {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return false;
            }
        }

        private async Task<bool> IsValidAsync(DeleteMotorcycleCommand request)
        {
            var motorcycle = await _motorcycleRepository.GetAsync(request.Id);

            if (motorcycle is null)
                _serviceContext.AddNotification($"A moto ID {request.Id} não existe");

            if (await ExistRentAsync(request.Id))
                _serviceContext.AddNotification($"Existe registro de locação para a moto ID {request.Id}");

            return !_serviceContext.HasNotification();
        }

        private async Task<bool> ExistRentAsync(string motorcycleId) =>
            !string.IsNullOrWhiteSpace((await _rentApi.GetByMotorcycleIdAsync(motorcycleId!))?.Id);
    }
}
