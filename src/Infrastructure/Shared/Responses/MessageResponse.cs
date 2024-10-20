using System.Text.Json.Serialization;

namespace Shared.Responses
{
    public class MessageResponse
    {
        public MessageResponse()
        {
            Message = "Dados inválidos";
        }

        public MessageResponse(string message)
        {
            Message = message;
        }

        [JsonPropertyName("mensagem")]
        public string Message { get; internal set; }
    }
}
