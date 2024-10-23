using MediatR;
using RentService.Application.Responses;
using System.Text.Json.Serialization;

namespace RentService.Application.Commands
{
    public class CreateRentCommand : IRequest<RentResponse?>
    {
        [JsonRequired]
        [JsonPropertyName("entregador_id")]
        public string DeliveryManId { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonRequired]
        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonRequired]
        [JsonPropertyName("data_previsao_termino")]
        public DateTime EstimatedEndDate { get; set; }

        [JsonRequired]
        [JsonPropertyName("plano")]
        public int Plan { get; set; }
    }
}
