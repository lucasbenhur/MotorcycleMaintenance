using MediatR;
using Microsoft.Extensions.Logging;
using RentService.Application.Mappers;
using RentService.Application.Queries;
using RentService.Application.Responses;
using RentService.Core.Repositories;
using Shared.ServiceContext;

namespace RentService.Application.Handlers
{
    public class GetRentByMotorcycleIdHandler : IRequestHandler<GetRentByMotorcycleIdQuery, RentResponse?>
    {
        private readonly IRentRepository _rentRepository;
        private readonly ILogger<GetRentByMotorcycleIdHandler> _logger;
        private readonly IServiceContext _serviceContext;

        public GetRentByMotorcycleIdHandler(
            IRentRepository rentRepository,
            ILogger<GetRentByMotorcycleIdHandler> logger,
            IServiceContext serviceContext)
        {
            _rentRepository = rentRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<RentResponse?> Handle(GetRentByMotorcycleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var rent = await _rentRepository.GetMotorcycleRentAsync(request.MotorcycleId);
                return RentMapper.Mapper.Map<RentResponse>(rent);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao consultar locações por Id";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }
    }
}
