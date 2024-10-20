using MediatR;
using MotorcycleService.Application.Responses;
using MotorcycleService.Core.Specs;

namespace MotorcycleService.Application.Queries
{
    public class GetAllMotorcyclesQuery : IRequest<ICollection<MotorcycleResponse>>
    {
        public MotorcycleSpecParams MotorcycleSpecParams { get; internal set; }

        public GetAllMotorcyclesQuery(MotorcycleSpecParams motorcycleSpecParams)
        {
            MotorcycleSpecParams = motorcycleSpecParams;
        }
    }
}
