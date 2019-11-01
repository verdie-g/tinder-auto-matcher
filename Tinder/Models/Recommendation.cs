using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class RecommendationResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; }
        [JsonPropertyName("data")]
        public RecommendationResponseData Data { get; set; }
    }

    public class RecommendationResponseData
    {
        [JsonPropertyName("results")]
        public IReadOnlyList<Recommendation> Results { get; set; }
    }

    public class Recommendation
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("user")]
        public UserRecommendation UserInfo { get; set; }
        [JsonPropertyName("facebook")]
        public Facebook FacebookInfo { get; set; }
        [JsonPropertyName("spotify")]
        public Spotify SpotifyInfo { get; set; }
        [JsonPropertyName("distance_mi")]
        public int DistanceMi { get; set; }
        [JsonPropertyName("content_hash")]
        public string ContentHash { get; set; }
        [JsonPropertyName("s_number")]
        public int SNumber { get; set; }
        [JsonPropertyName("teaser")]
        public Teaser TeaserInfo { get; set; }
        [JsonPropertyName("teasers")]
        public IReadOnlyList<Teaser> Teasers { get; set; }

        public class Spotify
        {
            [JsonPropertyName("spotify_connected")]
            public bool SpotifyConnected { get; set; }
            [JsonPropertyName("spotify_theme_track")]
            public ThemeTrack SpotifyThemeTrack { get; set; }

            public class ThemeTrack
            {
                [JsonPropertyName("id")]
                public string Id { get; set; }
                [JsonPropertyName("name")]
                public string Name { get; set; }
                [JsonPropertyName("preview_url")]
                public Uri PreviewUrl { get; set; }
                [JsonPropertyName("uri")]
                public Uri Uri { get; set; }
                [JsonPropertyName("album")]
                public Album AlbumInfo { get; set; }
                [JsonPropertyName("artist")]
                public Artist ArtistInfo { get; set; }

                public class Album
                {
                    [JsonPropertyName("id")]
                    public string Id { get; set; }
                    [JsonPropertyName("name")]
                    public string Name { get; set; }
                    [JsonPropertyName("images")]
                    public IReadOnlyList<Image> Images { get; set; }

                    public class Image
                    {
                        [JsonPropertyName("url")]
                        public Uri Url { get; set; }
                        [JsonPropertyName("width")]
                        public int Width { get; set; }
                        [JsonPropertyName("height")]
                        public int Height { get; set; }
                    }
                }

                public class Artist
                {
                    [JsonPropertyName("id")]
                    public string Id { get; set; }
                    [JsonPropertyName("name")]
                    public string Name { get; set; }
                }
            }
        }

        public class Teaser
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("string")]
            public string String { get; set; }

        }
    }
}