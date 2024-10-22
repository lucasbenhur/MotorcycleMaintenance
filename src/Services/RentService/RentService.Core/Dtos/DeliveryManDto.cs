using System.Text.Json.Serialization;

namespace RentService.Core.Dtos
{
    public class DeliveryManDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("nome")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; } = null!;

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; set; } = null!;

        [JsonPropertyName("tipo_cnh")]
        public string CnhType { get; set; } = null!;

        [JsonPropertyName("imagem_cnh")]
        public string CnhImage { get; set; } = null!;
    }
}
