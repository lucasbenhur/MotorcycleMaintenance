﻿using MediatR;
using System.Text.Json.Serialization;

namespace DeliveryManService.Application.Commands
{
    public class UpdateDeliveryManCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string? Id { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string? CnhImage { get; set; }
    }
}