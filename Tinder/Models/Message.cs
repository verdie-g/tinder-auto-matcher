using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class MessageResponse
    {
    }

    public class MessageRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}