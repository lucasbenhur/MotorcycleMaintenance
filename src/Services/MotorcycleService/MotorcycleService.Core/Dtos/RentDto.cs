using System.Text.Json.Serialization;

namespace MotorcycleService.Core.Dtos
{
    public class RentDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        [JsonPropertyName("valor_diaria")]
        public int? DailyValue { get; set; } = null!;

        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTime? DataDevolucao { get; set; }
    }
}
