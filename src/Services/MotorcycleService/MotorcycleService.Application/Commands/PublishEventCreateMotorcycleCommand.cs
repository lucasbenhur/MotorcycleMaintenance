using MediatR;
using MotorcycleService.Application.Responses;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Commands
{
    public class PublishEventCreateMotorcycleCommand : IRequest<PublishEventeCreateMotorcycleResponse>
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("placa")]
        public string Plate { get; set; }
    }
}
