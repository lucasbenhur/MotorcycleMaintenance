using MediatR;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Commands
{
    public class PublishEventCreateMotorcycleCommand : IRequest<bool>
    {
        [JsonPropertyName("identificador")]
        public string? Id { get; set; }

        [JsonPropertyName("ano")]
        public int? Year { get; set; }

        [JsonPropertyName("modelo")]
        public string? Model { get; set; }

        [JsonIgnore]
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
