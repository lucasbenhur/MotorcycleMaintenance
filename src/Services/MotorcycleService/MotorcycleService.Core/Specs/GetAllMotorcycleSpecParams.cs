using System.Text.Json.Serialization;

namespace MotorcycleService.Core.Specs
{
    public class GetAllMotorcycleSpecParams
    {
        public GetAllMotorcycleSpecParams()
        {
            Plate = null;
        }

        public GetAllMotorcycleSpecParams(string plate)
        {
            Plate = plate;
        }

        [JsonPropertyName("placa")]
        public string? Plate { get; set; }
    }
}
