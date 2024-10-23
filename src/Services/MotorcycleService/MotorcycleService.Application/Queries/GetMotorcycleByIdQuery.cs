using MediatR;
using MotorcycleService.Application.Responses;

namespace MotorcycleService.Application.Queries
{
    public class GetMotorcycleByIdQuery : IRequest<MotorcycleResponse?>
    {
        public string Id { get; internal set; }

        public GetMotorcycleByIdQuery(string id)
        {
            Id = id;
        }
    }
}
