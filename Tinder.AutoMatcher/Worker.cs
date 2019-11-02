using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tinder.Models;

namespace Tinder.AutoMatcher
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

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var authToken = _config.GetValue<string>("TinderClient:Token"); // X-Auth-Token
            var client = new TinderClient(authToken);

            await foreach (var teasedRec in GetTeasedRecommendations(client, cancellationToken: cancellationToken))
            {
                var like = await client.Like(teasedRec.UserInfo.Id, cancellationToken);
                if (like.Match != null)
                    _logger.LogInformation("You matched " + teasedRec.UserInfo.Name);
                else
                    _logger.LogError($"{teasedRec.UserInfo.Name} ({teasedRec.UserInfo.Id}) was not a match");
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendations(TinderClient client, TimeSpan? throttling = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            throttling ??= TimeSpan.FromMilliseconds(1000);

            while (!cancellationToken.IsCancellationRequested)
            {
                ISet<string> teaserPhotoIds = await GetTeaserPhotoIds(client, cancellationToken);
                await foreach (var teasedRec in GetTeasedRecommendationsWhileOneAppears(client, throttling.Value, teaserPhotoIds, cancellationToken))
                {
                    yield return teasedRec;
                }

                _logger.LogDebug("No teased recommendations were found in the last recommendations set. Refreshing teaser list");
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendationsWhileOneAppears(TinderClient client, TimeSpan throttling,
            ISet<string> teaserPhotoIds, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            bool matchOccuredInSet = true;
            while (matchOccuredInSet)
            {
                var recs = await GetRecommendations(client, cancellationToken);

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
                        await client.Pass(rec.UserInfo.Id, cancellationToken);
                    }

                    await Task.Delay(throttling, cancellationToken);
                }
            }
        }

        private async Task<ISet<string>> GetTeaserPhotoIds(TinderClient client, CancellationToken cancellationToken)
        {
            var teasers = await client.GetTeasers();
            return teasers
                .SelectMany(t => t.User.Photos)
                .Select(photo => photo.Id)
                .ToHashSet();
        }

        private async Task<IReadOnlyList<Recommendation>> GetRecommendations(TinderClient client, CancellationToken cancellationToken)
        {
            IReadOnlyList<Recommendation> recs = null;
            while ((recs = await client.GetRecommendations(cancellationToken)) == null)
            {
                _logger.LogDebug("No more recommendations. Retrying in a minute");
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }

            return recs;
        }
    }
}