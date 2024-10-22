using System.Text.Json.Serialization;

namespace RentService.Core.Dtos
{
    public class MotorcycleDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; } = null!;

        [JsonPropertyName("placa")]
        public string Plate { get; set; } = null!;
    }
}
