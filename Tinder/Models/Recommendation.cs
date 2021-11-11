using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class RecommendationResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; } = default!;
        [JsonPropertyName("data")]
        public RecommendationResponseData Data { get; set; } = default!;
    }

    public class RecommendationResponseData
    {
        [JsonPropertyName("results")]
        /// <summary>
        /// Can be null if there is no more recommendations.
        /// </summary>
        public IReadOnlyList<Recommendation>? Results { get; set; }
    }

    public class Recommendation
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;
        [JsonPropertyName("user")]
        public UserRecommendation UserInfo { get; set; } = default!;
        [JsonPropertyName("facebook")]
        public Facebook FacebookInfo { get; set; } = default!;

        [JsonPropertyName("instagram")]
        public Instagram InstagramInfo { get; set; } = default!;

        [JsonPropertyName("spotify")]
        public Spotify SpotifyInfo { get; set; } = default!;
        [JsonPropertyName("distance_mi")]
        public int DistanceMi { get; set; }
        [JsonPropertyName("content_hash")]
        public string ContentHash { get; set; } = default!;
        [JsonPropertyName("s_number")]
        public long SNumber { get; set; }
        [JsonPropertyName("teaser")]
        public Teaser TeaserInfo { get; set; } = default!;
        [JsonPropertyName("teasers")]
        public IReadOnlyList<Teaser> Teasers { get; set; } = default!;

        public class Spotify
        {
            [JsonPropertyName("spotify_connected")]
            public bool SpotifyConnected { get; set; }
            [JsonPropertyName("spotify_theme_track")]
            public ThemeTrack SpotifyThemeTrack { get; set; } = default!;

            public class ThemeTrack
            {
                [JsonPropertyName("id")]
                public string Id { get; set; } = default!;
                [JsonPropertyName("name")]
                public string Name { get; set; } = default!;
                [JsonPropertyName("preview_url")]
                public Uri PreviewUrl { get; set; } = default!;
                [JsonPropertyName("uri")]
                public Uri Uri { get; set; } = default!;
                [JsonPropertyName("album")]
                public Album AlbumInfo { get; set; } = default!;
                [JsonPropertyName("artist")]
                public Artist ArtistInfo { get; set; } = default!;

                public class Album
                {
                    [JsonPropertyName("id")]
                    public string Id { get; set; } = default!;
                    [JsonPropertyName("name")]
                    public string Name { get; set; } = default!;
                    [JsonPropertyName("images")]
                    public IReadOnlyList<Image> Images { get; set; } = default!;

                    public class Image
                    {
                        [JsonPropertyName("url")]
                        public Uri Url { get; set; } = default!;
                        [JsonPropertyName("width")]
                        public int Width { get; set; }
                        [JsonPropertyName("height")]
                        public int Height { get; set; }
                    }
                }

                public class Artist
                {
                    [JsonPropertyName("id")]
                    public string Id { get; set; } = default!;
                    [JsonPropertyName("name")]
                    public string Name { get; set; } = default!;
                }
            }
        }

        public class Teaser
        {
            [JsonPropertyName("type")]
            public string Type { get; set; } = default!;
            [JsonPropertyName("string")]
            public string String { get; set; } = default!;

        }
    }
}