using MediatR;
using System.Text.Json.Serialization;

namespace DeliveryManService.Application.Commands
{
    public class CreateDeliveryManCommand : IRequest<bool>
    {
        [JsonRequired]
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("nome")]
        public string Name { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; set; }

        [JsonRequired]
        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; set; } = null!;

        [JsonIgnore]
        private string _cnhType { get; set; } = null!;

        [JsonRequired]
        [JsonPropertyName("tipo_cnh")]
        public string CnhType
        {
            get
            {
                return _cnhType;
            }
            set
            {
                _cnhType = value
                    .Replace("+", "")
                    .ToUpper();
            }
        }

        [JsonRequired]
        [JsonPropertyName("imagem_cnh")]
        public string CnhImage { get; set; } = null!;
    }
}
