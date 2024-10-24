﻿using MediatR;
using System.Text.Json.Serialization;

namespace MotorcycleService.Application.Commands
{
    public class UpdateMotorcycleCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string? Id { get; set; }

        private string _plate { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("placa")]
        public string Plate
        {
            get
            {
                return _plate;
            }
            set
            {
                _plate = value.ToUpper();
            }
        }
    }
}
