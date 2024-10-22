using MediatR;
using System.Text.Json.Serialization;

namespace RentService.Application.Commands
{
    public class ReturnRentCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string? Id { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTime? ReturnDate { get; set; }
    }
}
