using MediatR;
using Microsoft.Extensions.Logging;
using MotorcycleService.Application.Mappers;
using MotorcycleService.Application.Queries;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Repositories;

namespace MotorcycleService.Application.Handlers
{
    public class GetAllMotorcyclesHandler : IRequestHandler<GetAllMotorcyclesQuery, ICollection<MotorcycleResponse>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<GetAllMotorcyclesHandler> _logger;

        public GetAllMotorcyclesHandler(
            IMotorcycleRepository motorcycleRepository,
            ILogger<GetAllMotorcyclesHandler> logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
        }

        public async Task<ICollection<MotorcycleResponse>> Handle(GetAllMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycles = await _motorcycleRepository.GetAllAsync(request.MotorcycleGetAllSpecParams);
                return MotorcycleMapper.Mapper.Map<ICollection<MotorcycleResponse>>(motorcycles);
            }
            catch (Exception ex)
            {
                var msg = string.Format("Ocorreu um erro ao consultar motos existentes . Detalhes: {Message}.", ex.Message);
                _logger.LogError(ex, msg);
                return Array.Empty<MotorcycleResponse>();
            }
        }
    }
}
