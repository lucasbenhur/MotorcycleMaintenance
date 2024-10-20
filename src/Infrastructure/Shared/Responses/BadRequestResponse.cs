using System.Text.Json.Serialization;

namespace Shared.Responses
{
    public class BadRequestResponse
    {
        public BadRequestResponse()
        {
            Message = "Dados inválidos";
        }

        public BadRequestResponse(string message)
        {
            Message = message;
        }

        [JsonPropertyName("mensagem")]
        public string Message { get; internal set; }
    }
}
