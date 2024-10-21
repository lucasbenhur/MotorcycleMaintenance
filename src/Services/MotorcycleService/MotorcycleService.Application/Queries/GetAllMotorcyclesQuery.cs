using MediatR;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Specs;

namespace MotorcycleService.Application.Queries
{
    public class GetAllMotorcyclesQuery : IRequest<ICollection<MotorcycleResponse>>
    {
        public GetAllMotorcyclesSpecParams GetAllMotorcyclesSpecParams { get; internal set; }

        public GetAllMotorcyclesQuery(GetAllMotorcyclesSpecParams specParams)
        {
            GetAllMotorcyclesSpecParams = specParams;
        }
    }
}
