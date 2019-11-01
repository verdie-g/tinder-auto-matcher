using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Pass
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}