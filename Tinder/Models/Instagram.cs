using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Instagram
    {
        [JsonPropertyName("last_fetch_time")]
        public DateTime LastFetchTime { get; set; }
        [JsonPropertyName("completed_initial_fetch")]
        public bool CompletedInitialFetch { get; set; }

        [JsonPropertyName("media_count")]
        public int MediaCount { get; set; }

        public IReadOnlyList<Photos> CommonPhotos { get; set; } = default!;
        public class Photos
        {
            [JsonPropertyName("image")]
            public string Image { get; set; } = default!;

            [JsonPropertyName("thumbnail")]
            public string Thumbnail { get; set; } = default!;

            [JsonPropertyName("ts")]
            public string Ts { get; set; } = default!;
        }

    }
}
