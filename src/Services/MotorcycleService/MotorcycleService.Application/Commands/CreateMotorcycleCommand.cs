using MediatR;
using MotorcycleService.Application.Responses;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Commands
{
    public class CreateMotorcycleCommand : IRequest<MotorcycleResponse>
    {
        [JsonPropertyName("identificador")]
        public string? Id { get; set; }

        [JsonPropertyName("ano")]
        public int? Year { get; set; }

        [JsonPropertyName("modelo")]
        public string? Model { get; set; }

        private string? _plate { get; set; }

        [JsonPropertyName("placa")]
        public string? Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                _plate = value?.ToUpper();
            }
        }
    }
}
