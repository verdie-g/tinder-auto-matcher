using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tinder.Models;

namespace Tinder.Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken _)
        {
            var authToken = _config.GetValue<string>("TinderClient:Token"); // X-Auth-Token
            var client = new TinderClient(authToken);

            await foreach (var teasedRec in GetTeasedRecommendations(client))
            {
                var like = await client.Like(teasedRec.UserInfo.Id);
                if (like.Match != null)
                    _logger.LogInformation("You matched " + teasedRec.UserInfo.Name);
                else
                    _logger.LogError($"{teasedRec.UserInfo.Name} ({teasedRec.UserInfo.Id}) was not a match");
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendations(TinderClient client, TimeSpan? throttling = null)
        {
            throttling ??= TimeSpan.FromMilliseconds(1000);

            while (true)
            {
                ISet<string> teaserPhotoIds = await GetTeaserPhotoIds(client);
                await foreach (var teasedRec in GetTeasedRecommendationsWhileOneAppears(client, throttling.Value, teaserPhotoIds))
                {
                    yield return teasedRec;
                }

                _logger.LogDebug("No teased recommendations were found in the last recommendations set. Refreshing teaser list");
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendationsWhileOneAppears(TinderClient client, TimeSpan throttling,
            ISet<string> teaserPhotoIds)
        {
            bool matchOccuredInSet = true;
            while (matchOccuredInSet)
            {
                var recs = await GetRecommendations(client);

                foreach (var rec in recs)
                {
                    if (rec.UserInfo.Photos.Any(photo => teaserPhotoIds.Contains(photo.Id)))
                    {
                        matchOccuredInSet = true;
                        yield return rec;
                    }
                    else
                    {
                        _logger.LogDebug("Pass " + rec.UserInfo.Name);
                        await client.Pass(rec.UserInfo.Id);
                    }

                    await Task.Delay(throttling);
                }
            }
        }

        private async Task<ISet<string>> GetTeaserPhotoIds(TinderClient client)
        {
            var teasers = await client.GetTeasers();
            return teasers
                .SelectMany(t => t.User.Photos)
                .Select(photo => photo.Id)
                .ToHashSet();
        }

        private async Task<IReadOnlyList<Recommendation>> GetRecommendations(TinderClient client)
        {
            IReadOnlyList<Recommendation> recs = null;
            while ((recs = await client.GetRecommendations()) == null)
            {
                _logger.LogDebug("No more recommendations. Retrying in a minute");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }

            return recs;
        }
    }
}