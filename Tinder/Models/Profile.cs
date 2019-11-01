using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class ProfileResponse
    {

        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; }
        [JsonPropertyName("data")]
        public Profile Profile { get; set; }
    }

    public class Profile
    {
        [JsonPropertyName("likes")]
        public Likes LikesInfo { get; set; }
        [JsonPropertyName("account")]
        public Account AccountInfo { get; set; }
        [JsonPropertyName("email")]
        public Email EmailInfo { get; set; }
        [JsonPropertyName("boost")]
        public Boost BoostInfo { get; set; }
        [JsonPropertyName("plus_control")]
        public PlusControl PlusControlInfo { get; set; }
        [JsonPropertyName("products")]
        public Product ProductsInfo { get; set; }
        [JsonPropertyName("user")]
        public UserSelf User { get; set; }
        [JsonPropertyName("instagram")]
        public Instagram InstagramInfo { get; set; }
        [JsonPropertyName("contact_cards")]
        public ContactCards ContactCardsInfo { get; set; }

        public class Likes
        {
            [JsonPropertyName("likes_remaining")]
            public int LikesRemaining { get; set; }
        }

        public class Account
        {
            [JsonPropertyName("account_phone_number")]
            public string AccountPhoneNumber { get; set; }
            [JsonPropertyName("is_email_verified")]
            public bool IsEmailVerified { get; set; }
            [JsonPropertyName("account_email")]
            public bool AccountEmail { get; set; }
        }

        public class Email
        {
            [JsonPropertyName("email")]
            public string EmailAddress { get; set; }

            public class Settings
            {
                [JsonPropertyName("promotions")]
                public bool Promotions { get; set; }
                [JsonPropertyName("messages")]
                public bool Messages { get; set; }
                [JsonPropertyName("new_matches")]
                public bool NewMatches { get; set; }
            }
        }

        public class ContactCards
        {
            [JsonPropertyName("populated_cards")]
            public IReadOnlyList<PopulatedCard> PopulatedCards { get; set; }
            [JsonPropertyName("available_cards")]
            public IReadOnlyList<string> AvailableCards { get; set; }

            public class PopulatedCard
            {
                [JsonPropertyName("contact_id")]
                public string ContactId { get; set; }
                [JsonPropertyName("contact_type")]
                public string ContactType { get; set; }
                [JsonPropertyName("default")]
                public bool Default { get; set; }
            }
        }

        public class Boost
        {
            [JsonPropertyName("duration")]
            public int Duration { get; set; }
            [JsonPropertyName("allotment")]
            public int Allotment { get; set; }
            [JsonPropertyName("allotment_used")]
            public int AllotmentUsed { get; set; }
            [JsonPropertyName("allotment_remaining")]
            public int AllotmentRemaining { get; set; }
            [JsonPropertyName("internal_remaining")]
            public int InternalRemaining { get; set; }
            [JsonPropertyName("purchased_remaining")]
            public int PurchasedRemaining { get; set; }
            [JsonPropertyName("remaining")]
            public int Remaining { get; set; }
            [JsonPropertyName("super_boost_purchased_remaining")]
            public int SuperBoostPurchasedRemaining { get; set; }
            [JsonPropertyName("super_boost_remaining")]
            public int SuperBoostRemaining { get; set; }
            [JsonPropertyName("boost_refresh_amount")]
            public int BoostRefreshAmount { get; set; }
            [JsonPropertyName("boost_refresh_interval")]
            public int BoostRefreshInterval { get; set; }
            [JsonPropertyName("boost_refresh_interval_unit")]
            public string BoostRefreshIntervalUnit { get; set; }
            // [JsonPropertyName("purchases")]
            // public IReadOnlyList<> BoostRefreshIntervalUnit { get; set; }
        }

        public class PlusControl
        {
        }

        public class Product
        {
            // TODO
        }

        public class Purchase
        {
            // [JsonPropertyName("purchases")]
            // public IReadOnlyList<> Purchases { get; set; }
            [JsonPropertyName("subscription_expired")]
            public bool SubscriptionExpired { get; set; }
        }
    }
}