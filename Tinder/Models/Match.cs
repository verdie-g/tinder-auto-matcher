using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tinder.Models
{
    public class MatchesResponse
    {
        [JsonPropertyName("meta")]
        public ResponseMeta Meta { get; set; }
        [JsonPropertyName("data")]
        public MatchesResponseData Data { get; set; }
    }

    public class MatchesResponseData
    {
        [JsonPropertyName("matches")]
        public IReadOnlyList<Match> Matches { get; set; }
    }

    public class Match
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("seen")]
        public Seen SeenInfo { get; set; }
        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
        [JsonPropertyName("common_friend_count")]
        public int CommonFriendCount { get; set; }
        [JsonPropertyName("common_like_count")]
        public int CommonLikeCount { get; set; }
        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonPropertyName("dead")]
        public bool Dead { get; set; }
        [JsonPropertyName("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
        [JsonPropertyName("message_count")]
        public int MessageCount { get; set; }
        [JsonPropertyName("messages")]
        public IReadOnlyList<Message> Messages { get; set; }
        [JsonPropertyName("muted")]
        public bool Muted { get; set; }
        [JsonPropertyName("participants")]
        public IReadOnlyList<string> Particpants { get; set; }
        [JsonPropertyName("pending")]
        public bool Pending { get; set; }
        [JsonPropertyName("is_super_like")]
        public bool IsSuperLike { get; set; }
        [JsonPropertyName("is_boost_match")]
        public bool IsBoostMatch { get; set; }
        [JsonPropertyName("is_super_boost_match")]
        public bool IsSuperBoostMatch { get; set; }
        [JsonPropertyName("is_experiences_match")]
        public bool IsExperiencesMatch { get; set; }
        [JsonPropertyName("is_fast_match")]
        public bool IsFastMatch { get; set; }
        [JsonPropertyName("following")]
        public bool Following { get; set; }
        [JsonPropertyName("following_moments")]
        public bool FollowingMoments { get; set; }
        [JsonPropertyName("read_receipt")]
        public ReadReceipt ReadReceiptInfo { get; set; }
        [JsonPropertyName("person")]
        public UserProfile Person { get; set; }

        public class Seen
        {
            [JsonPropertyName("match_seen")]
            public bool MatchSeen { get; set; }
            [JsonPropertyName("last_seen_msg_id")]
            public string LastSeenMsgId { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("_id")]
            public string Id { get; set; }
            [JsonPropertyName("match_id")]
            public string MatchId { get; set; }
            [JsonPropertyName("sent_date")]
            public string SentDate { get; set; }
            [JsonPropertyName("message")]
            public string Content { get; set; }
            [JsonPropertyName("to")]
            public string To { get; set; }
            [JsonPropertyName("from")]
            public string From { get; set; }
            [JsonPropertyName("timestamp")]
            public int Timestamp { get; set; }
        }

        public class ReadReceipt
        {
            public bool Enabled { get; set; }
        }
    }
}