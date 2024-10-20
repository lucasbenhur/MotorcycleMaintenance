using MediatR;
using MotorcycleService.Application.Responses;

namespace MotorcycleService.Application.Commands
{
    public class CreateMotorcycleCommand : IRequest<CreateMotorcycleResponse>
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
