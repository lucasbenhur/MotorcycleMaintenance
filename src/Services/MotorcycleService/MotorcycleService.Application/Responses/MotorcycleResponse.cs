using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Responses
{
    public class MotorcycleResponse
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
