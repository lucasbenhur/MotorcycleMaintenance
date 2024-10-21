using System.Text.Json.Serialization;

namespace MotorcycleService.Core.Specs
{
    public class GetAllMotorcyclesSpecParams
    {
        public GetAllMotorcyclesSpecParams()
        {
            Plate = null;
        }

        public GetAllMotorcyclesSpecParams(
            string? plate = null)
        {
            Plate = plate;
        }

        [JsonPropertyName("placa")]
        public string? Plate { get; set; }
    }
}
