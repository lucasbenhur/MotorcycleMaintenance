﻿using System.Text.Json.Serialization;
using MediatR;
using RentService.Application.Responses;

namespace RentService.Application.Commands
{
    public class RentMotorcycleCommand : IRequest<RentalResponse?>
    {
        [JsonPropertyName("entregador_id")]
        public string? DeliveryManId { get; set; }

        [JsonPropertyName("moto_id")]
        public string? MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime? EstimatedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int? Plan { get; set; }
    }
}
