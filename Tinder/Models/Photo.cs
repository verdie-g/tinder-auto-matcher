using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Photo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;
        [JsonPropertyName("url")]
        public Uri Url { get; set; } = default!;
        [JsonPropertyName("crop_info")]
        public Crop CropInfo { get; set; } = default!;
        [JsonPropertyName("processedFiles")]
        public IReadOnlyList<Processed> ProcessedFiles { get; set; } = default!;
        [JsonPropertyName("last_update_time")]
        public DateTime LastUpdateTime { get; set; }
        [JsonPropertyName("file_name")]
        public string FileName { get; set; } = default!;
        [JsonPropertyName("extension")]
        public string Extension { get; set; } = default!;
        [JsonPropertyName("webp_qf")]
        public IReadOnlyList<int> WebpQf { get; set; } = default!;

        public class Crop
        {
            [JsonPropertyName("user")]
            public Content User { get; set; } = default!;
            [JsonPropertyName("algo")]
            public Content Algo { get; set; } = default!;
            [JsonPropertyName("processed_by_bullseye")]
            public bool ProcessedByBullseye { get; set; }
            [JsonPropertyName("user_customized")]
            public bool UserCustomized { get; set; }

            public class Content
            {
                [JsonPropertyName("width_pct")]
                public float WidthPct { get; set; }
                [JsonPropertyName("x_offset_pct")]
                public float XOffsetPct { get; set; }
                [JsonPropertyName("height_pct")]
                public float HeightPct { get; set; }
                [JsonPropertyName("y_offset_pct")]
                public float YOffsetPct { get; set; }
            }
        }

        public class Processed
        {
            [JsonPropertyName("url")]
            public Uri Url { get; set; } = default!;
            [JsonPropertyName("height")]
            public int Height { get; set; }
            [JsonPropertyName("width")]
            public int Width { get; set; }
        }
    }
}