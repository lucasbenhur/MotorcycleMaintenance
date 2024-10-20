using MediatR;
using MotorcycleService.Application.Responses;

namespace MotorcycleService.Application.Commands
{
    public class CreateMotorcycleCommand : IRequest<MotorcycleResponse>
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        private string _plate { get; set; }
        public string Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                _plate = value.ToUpper();
            }
        }
    }
}
