using System;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class MessageResponse
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = default!;
        [JsonPropertyName("from")]
        public string From { get; set; } = default!;
        [JsonPropertyName("to")]
        public string To { get; set; } = default!;
        [JsonPropertyName("match_id")]
        public string MatchId { get; set; } = default!;
        [JsonPropertyName("sent_date")]
        public DateTime SentDate { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
        // [JsonPropertyName("media")]
        // public Media Media { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
    }

    public class MessageRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = default!;
    }
}