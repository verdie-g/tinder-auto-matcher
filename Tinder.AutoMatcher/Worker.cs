using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tinder.Models;

namespace Tinder.AutoMatcher
{
    public class Worker : BackgroundService
    {
        private readonly ITinderClient _client;
        private readonly ILogger<Worker> _logger;

        public Worker(ITinderClient tinderClient, ILogger<Worker> logger)
        {
            _client = tinderClient;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                ISet<string> teaserPhotoIds = await GetTeaserPhotoIds(cancellationToken);
                await foreach (var teasedRec in GetTeasedRecommendations(teaserPhotoIds, cancellationToken))
                {
                    var like = await _client.Like(teasedRec.UserInfo.Id, cancellationToken);
                    if (like.Match != null)
                        _logger.LogInformation("You matched " + teasedRec.UserInfo.Name);
                    else
                        _logger.LogError($"{teasedRec.UserInfo.Name} ({teasedRec.UserInfo.Id}) was not a match");
                }

                _logger.LogInformation("No more teased recommendations found. Pausing for 15 minutes");
                await Task.Delay(TimeSpan.FromMinutes(15));
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendations(ISet<string> teaserPhotoIds, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            bool matchOccuredInSet = false;
            do
            {
                var recs = await GetRecommendations(cancellationToken);
                foreach (var rec in recs)
                {
                    if (rec.UserInfo.Photos.Any(photo => teaserPhotoIds.Contains(photo.Id)))
                    {
                        matchOccuredInSet = true;
                        yield return rec;
                    }
                }
            } while (matchOccuredInSet);
        }

        private async Task<ISet<string>> GetTeaserPhotoIds(CancellationToken cancellationToken)
        {
            var teasers = await _client.GetTeasers();
            return teasers
                .SelectMany(t => t.User.Photos)
                .Select(photo => photo.Id)
                .ToHashSet();
        }

        private async Task<IReadOnlyList<Recommendation>> GetRecommendations(CancellationToken cancellationToken)
        {
            IReadOnlyList<Recommendation> recs = null;
            while ((recs = await _client.GetRecommendations(cancellationToken)) == null)
            {
                _logger.LogDebug("No more recommendations. Retrying in 5 minutes");
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
            }

            return recs;
        }
    }
}