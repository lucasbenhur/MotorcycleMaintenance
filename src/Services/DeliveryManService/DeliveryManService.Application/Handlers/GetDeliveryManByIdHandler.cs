using DeliveryManService.Application.Mappers;
using DeliveryManService.Application.Queries;
using DeliveryManService.Application.Responses;
using DeliveryManService.Core.Repositories;
using MediatR;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace DeliveryManService.Application.Handlers
{
    public class GetDeliveryManByIdHandler : IRequestHandler<GetDeliveryManByIdQuery, DeliveryManResponse>
    {
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public GetDeliveryManByIdHandler(
            IDeliveryManRepository deliveryManRepository,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            _deliveryManRepository = deliveryManRepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<DeliveryManResponse> Handle(GetDeliveryManByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryMan = await _deliveryManRepository.GetAsync(request.Id);
                return DeliveryManMapper.Mapper.Map<DeliveryManResponse>(deliveryMan);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao consultar entregadores por Id";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return null;
            }
        }
    }
}
