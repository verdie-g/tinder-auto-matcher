using System.Text.Json.Serialization;
using Tinder.Models.Converters;

namespace Tinder.Models
{
    public class Like
    {
        [JsonPropertyName("match")]
        [JsonConverter(typeof(JsonFalseOrObjectConverter<Match>))]
        /// <summary>
        /// Null if there was not match.
        /// </summary>
        public Match Match { get; set; } = default!;
        [JsonPropertyName("likes_remaining")]
        public int LikesRemaining { get; set; }
    }
}