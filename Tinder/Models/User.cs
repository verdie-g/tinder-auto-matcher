using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class UserBase
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = default!;
        [JsonPropertyName("photos")]
        public IReadOnlyList<Photo> Photos { get; set; } = default!;
    }

    public class UserProfile : UserBase
    {
        [JsonPropertyName("bio")]
        public string Bio { get; set; } = default!;
        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("gender")]
        public Gender GenderInfo { get; set; } = default!;
        [JsonPropertyName("schools")]
        public IReadOnlyList<School> Schools { get; set; } = default!;
        [JsonPropertyName("companies")]
        public IReadOnlyList<Company> Companies { get; set; } = default!;
        [JsonPropertyName("jobs")]
        public IReadOnlyList<Job> Jobs { get; set; } = default!;

        public enum Gender
        {
            Male = 0,
            Female = 1,
        }

        public class School
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = default!;
        }

        public class Company
        {
            [JsonPropertyName("name")]
            public string Name { get; set; } = default!;
        }

        public class Job
        {
            [JsonPropertyName("company")]
            public Company Company { get; set; } = default!;
        }
    }

    public class UserRecommendation : UserProfile
    {
        [JsonPropertyName("show_gender_on_profile")]
        public bool ShowGenderOnProfile { get; set; }
        [JsonPropertyName("new_user_badge_enabled")]
        public bool NewUserBadgeEnabled { get; set; }
    }

    public class UserSelf : UserRecommendation
    {
        [JsonPropertyName("age_filter_max")]
        public int AgeFilterMax { get; set; }
        [JsonPropertyName("age_filter_min")]
        public int AgeFilterMin { get; set; }
        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
        [JsonPropertyName("crm_id")]
        public string CrmId { get; set; } = default!;
        [JsonPropertyName("discoverable")]
        public bool Discoverable { get; set; }
        [JsonPropertyName("interests")]
        public IReadOnlyList<Facebook.Interest> Interests { get; set; } = default!;
        [JsonPropertyName("distance_filter")]
        public int DistanceFilter { get; set; }
        [JsonPropertyName("gender_filter")]
        public Gender GenderFilter { get; set; } = default!;
        [JsonPropertyName("photos_processing")]
        public bool PhotosProcessing { get; set; }
        [JsonPropertyName("photo_optimizer_enabled")]
        public bool PhotoOptimizerEnabled { get; set; }
        [JsonPropertyName("ping_time")]
        public DateTime PingTime { get; set; }
        // public IReadOnlyList<> Badges { get; set; }
        [JsonPropertyName("phone_id")]
        public string PhoneId { get; set; } = default!;
        [JsonPropertyName("interested_in")]
        public IReadOnlyList<Gender> InterestedIn { get; set; } = default!;
        // public Geolocation pos { get; set; } = default!;
        [JsonPropertyName("auto_play_video")]
        public string AutoPlayVideo { get; set; } = default!;
        [JsonPropertyName("top_picks_discoverable")]
        public bool TopPicksDiscoverable { get; set; }
        [JsonPropertyName("photo_tagging_enabled")]
        public bool PhotoTaggingEnabled { get; set; }
        [JsonPropertyName("recommended_sort_discoverable")]
        public bool RecommendedSortDiscoverable { get; set; }
    }
}