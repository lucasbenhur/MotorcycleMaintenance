using DeliveryManService.Application.Mappers;
using DeliveryManService.Application.Queries;
using DeliveryManService.Application.Responses;
using DeliveryManService.Core.Repositories;
using MediatR;
using Shared.AppLog.Services;
using Shared.ServiceContext;

namespace DeliveryManService.Application.Handlers
{
    public class GetAllDeliveryMenHandler : IRequestHandler<GetAllDeliveryMenQuery, ICollection<DeliveryManResponse>>
    {
        private readonly IDeliveryManRepository _deliveryManrepository;
        private readonly IAppLogger _logger;
        private readonly IServiceContext _serviceContext;

        public GetAllDeliveryMenHandler(
            IDeliveryManRepository deliveryManrepository,
            IAppLogger logger,
            IServiceContext serviceContext)
        {
            _deliveryManrepository = deliveryManrepository;
            _logger = logger;
            _serviceContext = serviceContext;
        }

        public async Task<ICollection<DeliveryManResponse>> Handle(GetAllDeliveryMenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryMen = await _deliveryManrepository.GetAllAsync(request.GetAllDeliveryMenSpecParams);
                return DeliveryManMapper.Mapper.Map<ICollection<DeliveryManResponse>>(deliveryMen);
            }
            catch (Exception ex)
            {
                var msg = $"Ocorreu um erro ao consultar motos";
                _logger.LogError(ex, msg);
                _serviceContext.AddNotification(msg);
                return Array.Empty<DeliveryManResponse>();
            }
        }
    }
}
