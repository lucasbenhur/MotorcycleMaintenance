using DeliveryManService.Application.Responses;
using MediatR;

namespace DeliveryManService.Application.Queries
{
    public class GetDeliveryManByIdQuery : IRequest<DeliveryManResponse>
    {
        public string Id { get; internal set; }

        public GetDeliveryManByIdQuery(string id)
        {
            Id = id;
        }
    }
}
