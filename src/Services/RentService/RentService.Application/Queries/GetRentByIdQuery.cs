using MediatR;
using RentService.Application.Responses;

namespace RentService.Application.Queries
{
    public class GetRentByIdQuery : IRequest<RentResponse?>
    {
        public string Id { get; internal set; }

        public GetRentByIdQuery(string id)
        {
            Id = id;
        }
    }
}
