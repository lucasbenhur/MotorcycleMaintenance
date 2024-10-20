using MediatR;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Specs;

namespace MotorcycleService.Application.Queries
{
    public class GetAllMotorcyclesQuery : IRequest<ICollection<MotorcycleResponse>>
    {
        public GetAllMotorcycleSpecParams MotorcycleGetAllSpecParams { get; internal set; }

        public GetAllMotorcyclesQuery(GetAllMotorcycleSpecParams specParams)
        {
            MotorcycleGetAllSpecParams = specParams;
        }
    }
}
