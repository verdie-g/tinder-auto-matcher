using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Tinder.Exceptions;
using Tinder.Models;

namespace Tinder
{
    public class TinderClient
    {
        private readonly HttpClient _httpClient;

        public TinderClient(string authToken)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.gotinder.com/"),
            };

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Tinder/7.5.3 (iPhone; iOS 10.3.2; Scale/2.00)");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", authToken);
        }

        public async Task<IEnumerable<Recommendation>> GetRecommendations()
        {
            var res = await Get<RecommendationResponse>("v2/recs/core");
            return res.Data.Results;
        }

        public async Task<TeaserData> GetTeaser()
        {
            var res = await Get<TeaserResponse>("v2/fast-match/teaser");
            return res.Data;
        }

        public async Task<IEnumerable<Teaser>> GetTeasers()
        {
            var res = await Get<TeasersResponse>("v2/fast-match/teasers");
            return res.Data.Results;
        }

        public async Task<IReadOnlyList<Match>> GetMatches(int count = 60, bool isTinderU = false)
        {
            var res = await Get<MatchesResponse>($"v2/matches?is_tinder_u={isTinderU}&message=1&count={count}");
            return res.Data.Matches;
        }

        public async Task Message(string userId, string message)
        {
            await Post<MessageRequest, MessageResponse>("user/matches/" + userId, new MessageRequest { Message = message });
        }

        public async Task Ping(Geolocation geolocation)
        {
            await Post<Geolocation, PingResponse>("user/ping", geolocation);
        }

        public async Task Travel(Geolocation geolocation)
        {
            await Post<Geolocation, TravelResponse>("passport/user/travel", geolocation);
        }

        public Task<Like> Like(string userId)
        {
            return Get<Like>("like/" + userId);
        }

        public async Task Pass(string userId)
        {
            await Get<Pass>("pass/" + userId);
        }

        public async Task Unmatch(string matchId)
        {
            await Delete<UnmatchResponse>("user/matches/" + matchId);
        }

        public async Task<Profile> GetProfile()
        {
            var res = await Get<ProfileResponse>("profile");
            return res.Profile;
        }

        public async Task<Meta> GetMetadatas()
        {
            var res = await Get<Meta>("v2/meta");
            return res;
        }

        private Task<TResponse> Get<TResponse>(string requestUri)
        {
            return Send<TResponse>(new HttpRequestMessage(HttpMethod.Get, requestUri));
        }

        private Task<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest payload)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var jsonPayload = JsonSerializer.Serialize(payload);
            msg.Content = new StringContent(jsonPayload);
            return Send<TResponse>(msg);
        }

        private Task<TResponse> Delete<TResponse>(string requestUri)
        {
            return Send<TResponse>(new HttpRequestMessage(HttpMethod.Delete, requestUri));
        }

        private async Task<TResponse> Send<TResponse>(HttpRequestMessage msg)
        {
            var res = await _httpClient.SendAsync(msg);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new TinderAuthenticationException("Invalid or expired token");
                }
                throw new TinderException(json);
            }

            return Deserialize<TResponse>(json);
        }

        private T Deserialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception e)
            {
                throw new TinderSerializationException($"Couldn't deserialize response: ${json}", e);
            }
        }
    }
}
