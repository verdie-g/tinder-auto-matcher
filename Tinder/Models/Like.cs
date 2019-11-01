using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Like
    {
        [JsonPropertyName("match")]
        public bool Match { get; set; }
    }
}