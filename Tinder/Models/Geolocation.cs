using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class Geolocation
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
    }
}