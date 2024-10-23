using MediatR;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class GetMotorcycleByIdHandler : IRequestHandler<GetMotorcycleByIdQuery, MotorcycleResponse?>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public GetMotorcycleByIdHandler(
            IMotorcycleRepository motorcycleRepository,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<MotorcycleResponse?> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleRepository.GetAsync(request.Id);
                return MotorcycleMapper.Mapper.Map<MotorcycleResponse>(motorcycle);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao consultar Motos com ID {request.Id}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }
    }
}
