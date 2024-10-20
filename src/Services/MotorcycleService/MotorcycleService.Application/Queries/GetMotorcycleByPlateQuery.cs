using MediatR;
using MotorcycleService.Application.Responses;

namespace MotorcycleService.Application.Queries
{
    public class GetMotorcycleByPlateQuery : IRequest<MotorcycleResponse>
    {
        public string Plate { get; internal set; }

        public GetMotorcycleByPlateQuery(string plate)
        {
            Plate = plate;
        }
    }
}
