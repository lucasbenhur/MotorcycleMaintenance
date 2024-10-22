using RentService.Core.Entities;
using System.Text.Json.Serialization;

namespace RentService.Application.Responses
{
    public class RentResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; } = null!;

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; } = null!;

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }

        [JsonPropertyName("plano")]
        private RentPlan Plan { get; set; }
    }
}
