using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class ResponseMeta
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}