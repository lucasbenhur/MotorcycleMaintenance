using MediatR;
using RentService.Application.Mappers;
using RentService.Application.Queries;
using RentService.Application.Responses;
using RentService.Core.Repositories;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace RentService.Application.Handlers
{
    public class GetRentByMotorcycleIdHandler : IRequestHandler<GetRentByMotorcycleIdQuery, RentResponse?>
    {
        private readonly IRentRepository _rentRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public GetRentByMotorcycleIdHandler(
            IRentRepository rentRepository,
            IAppLogger logger,
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
