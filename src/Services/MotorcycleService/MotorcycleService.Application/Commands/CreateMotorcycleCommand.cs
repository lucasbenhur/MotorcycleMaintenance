using MediatR;
using MotorcycleService.Application.Responses;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Commands
{
    public class CreateMotorcycleCommand : IRequest<MotorcycleResponse?>
    {
        [JsonRequired]
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonRequired]
        [JsonPropertyName("modelo")]
        public string Model { get; set; } = null!;

        [JsonIgnore]
        private string _plate { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("placa")]
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
