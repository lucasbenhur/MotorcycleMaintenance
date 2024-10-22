using MediatR;
using RentService.Application.Responses;

namespace RentService.Application.Queries
{
    public class GetRentByMotorcycleIdQuery : IRequest<RentResponse?>
    {
        public string MotorcycleId { get; internal set; }

        public GetRentByMotorcycleIdQuery(string motorcycleId)
        {
            MotorcycleId = motorcycleId;
        }
    }
}
