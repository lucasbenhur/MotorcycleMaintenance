using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using RentService.Core.Entities;

namespace RentService.Application.Responses
{
    public class RentalResponse
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
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
        private RentalPlan Plan { get; set; }
    }
}
