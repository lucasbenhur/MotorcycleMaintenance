using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;
using Shared.ServiceContext;

namespace MotorcycleService.Application.Handlers
{
    public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcyclesQuery, ICollection<MotorcycleResponse>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<GetAllMotorcyclesHandler> _logger;
        private readonly IServiceContext _serviceContext;

        public GetAllMotorcyclesHandler(
            IMotorcycleRepository motorcycleRepository,
            ILogger<GetAllMotorcyclesHandler> logger,
            IServiceContext serviceContext)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<ICollection<MotorcycleResponse>> Handle(GetAllMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycles = await _motorcycleRepository.GetAllAsync(request.GetAllMotorcyclesSpecParams);
                return MotorcycleMapper.Mapper.Map<ICollection<MotorcycleResponse>>(motorcycles);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao consultar motos. Detalhes: {ex.Message}";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return Array.Empty<MotorcycleResponse>();
            }
        }
    }
}
