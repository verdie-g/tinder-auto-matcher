using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Facebook
    {
        // public IReadOnlyList<string> CommonConnections { get; set; }
        [JsonPropertyName("connection_count")]
        public int ConnectionCount { get; set; }
        public IReadOnlyList<Interest> CommonInterests { get; set; } = default!;

        public class Interest
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = default!;
            [JsonPropertyName("name")]
            public string Name { get; set; } = default!;
        }
    }
}