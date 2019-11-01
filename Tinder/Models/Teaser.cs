using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class TeasersResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; }
        [JsonPropertyName("data")]
        public TeasersResponseData Data { get; set; }
    }

    public class TeasersResponseData
    {
        [JsonPropertyName("results")]
        public IReadOnlyList<Teaser> Results { get; set; }
    }

    public class Teaser
    {
        [JsonPropertyName("user")]
        public UserBase User { get; set; }
    }

    public class TeaserResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; }
        [JsonPropertyName("data")]
        public TeaserData Data { get; set; }
    }

    public class TeaserData
    {
        [JsonPropertyName("is_range")]
        public bool IsRange { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("teaser_url")]
        public Uri TeaserUrl { get; set; }
    }
}