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
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<Worker> _logger;

        public Worker(ITinderClient tinderClient, IHostApplicationLifetime appLiftetime, ILogger<Worker> logger)
        {
            _client = tinderClient;
            _appLifetime = appLiftetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                await MatchTeasedRecommendations(cancellationToken);
            }
            catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _appLifetime.StopApplication();
            }
        }

        private async Task MatchTeasedRecommendations(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int likesNb = await GetLikesNumber(cancellationToken);
                _logger.LogInformation($"{likesNb} people liked you");

                ISet<string> teaserPhotoIds = await GetTeaserPhotoIds(cancellationToken);
                await foreach (var teasedRec in GetTeasedRecommendations(teaserPhotoIds, cancellationToken))
                {
                    var like = await _client.Like(teasedRec.UserInfo.Id, cancellationToken);
                    if (like.Match != null)
                        _logger.LogInformation("You matched " + teasedRec.UserInfo.Name);
                    else
                        _logger.LogError($"{teasedRec.UserInfo.Name} ({teasedRec.UserInfo.Id}) was not a match");
                }

                _logger.LogInformation("No more teased recommendations found. Pausing for 2 hours");
                await Task.Delay(TimeSpan.FromHours(2), cancellationToken);
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendations(ISet<string> teaserPhotoIds, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            bool matchOccuredInSet;
            do
            {
                matchOccuredInSet = false;
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
            var teasers = await _client.GetTeasers(cancellationToken);
            return teasers
                .SelectMany(t => t.User.Photos)
                .Select(photo => photo.Id)
                .ToHashSet();
        }

        private async Task<int> GetLikesNumber(CancellationToken cancellationToken)
        {
            var teaser = await _client.GetTeaser(cancellationToken);
            return teaser.Count;
        }

        private async Task<IReadOnlyList<Recommendation>> GetRecommendations(CancellationToken cancellationToken)
        {
            IReadOnlyList<Recommendation>? recs;
            while ((recs = await _client.GetRecommendations(cancellationToken)) == null)
            {
                _logger.LogDebug("No more recommendations. Retrying in 1 hour");
                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }

            return recs;
        }
    }
}