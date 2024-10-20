using System.Text.Json.Serialization;

namespace MotorcycleService.Core.Specs
{
    public class GetAllMotorcycleSpecParams
    {
        [JsonPropertyName("placa")]
        public string? Plate { get; set; }
    }
}
