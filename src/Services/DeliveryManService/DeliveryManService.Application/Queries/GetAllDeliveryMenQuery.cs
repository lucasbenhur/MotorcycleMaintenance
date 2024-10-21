using DeliveryManService.Application.Responses;
using DeliveryManService.Core.Specs;
using MediatR;

namespace DeliveryManService.Application.Queries
{
    public class GetAllDeliveryMenQuery : IRequest<ICollection<DeliveryManResponse>>
    {
        public GetAllDeliveryMenSpecParams GetAllDeliveryMenSpecParams { get; set; }

        public GetAllDeliveryMenQuery(GetAllDeliveryMenSpecParams specParams)
        {
            GetAllDeliveryMenSpecParams = specParams;
        }
    }
}
