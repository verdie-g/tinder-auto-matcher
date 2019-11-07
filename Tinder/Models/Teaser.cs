using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class TeasersResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; } = default!;
        [JsonPropertyName("data")]
        public TeasersResponseData Data { get; set; } = default!;
    }

    public class TeasersResponseData
    {
        [JsonPropertyName("results")]
        public IReadOnlyList<Teaser> Results { get; set; } = default!;
    }

    public class Teaser
    {
        [JsonPropertyName("user")]
        public UserBase User { get; set; } = default!;
    }

    public class TeaserResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; } = default!;
        [JsonPropertyName("data")]
        public TeaserData Data { get; set; } = default!;
    }

    public class TeaserData
    {
        [JsonPropertyName("is_range")]
        public bool IsRange { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("teaser_url")]
        public Uri TeaserUrl { get; set; } = default!;
    }
}