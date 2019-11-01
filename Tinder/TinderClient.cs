using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

        private async Task<TResponse> Get<TResponse>(string requestUri)
        {
            var res = await _httpClient.GetAsync(requestUri);
            var json = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                throw new TinderException(json);
            }

            try
            {
                return JsonSerializer.Deserialize<TResponse>(json);
            }
            catch (Exception)
            {
                throw new TinderException(json);
            }
        }

        private async Task<TResponse> Post<TRequest, TResponse>(string requestUri, TRequest payload)
        {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var res = await _httpClient.PostAsync(requestUri, new StringContent(jsonPayload));
            var jsonResponse = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
            {
                // status: int + error: string
                throw new TinderException(jsonResponse);
            }

            try
            {
                return JsonSerializer.Deserialize<TResponse>(jsonResponse);
            }
            catch (Exception)
            {
                throw new TinderException(jsonResponse);
            }
        }
    }
}
