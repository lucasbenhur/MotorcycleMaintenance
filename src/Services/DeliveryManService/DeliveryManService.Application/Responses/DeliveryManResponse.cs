using System.Text.Json.Serialization;

namespace DeliveryManService.Application.Responses
{
    public class DeliveryManResponse
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public string CnhType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string CnhImage { get; set; }
    }
}
