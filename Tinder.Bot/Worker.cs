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
            await foreach (var rec in GetTeasedRecommendations(client))
            {
                var like = await client.Like(rec.UserInfo.Id);
                if (like.Match)
                    Console.WriteLine("You matched " + rec.UserInfo.Name);
                else
                    Console.WriteLine("Error");
            }
        }

        private async IAsyncEnumerable<Recommendation> GetTeasedRecommendations(TinderClient client, TimeSpan? throttling = null)
        {
            throttling ??= TimeSpan.FromMilliseconds(1000);

            var teasers = await client.GetTeasers();
            var teaserPhotoIds = teasers
                .SelectMany(t => t.User.Photos)
                .Select(photo => photo.Id)
                .ToHashSet();

            await foreach (var rec in GetRecommendationsStream(client))
            {
                if (rec.UserInfo.Photos.Any(photo => teaserPhotoIds.Contains(photo.Id)))
                {
                    yield return rec;
                }
                else
                {
                    await client.Pass(rec.UserInfo.Id);
                    Console.WriteLine("Pass " + rec.UserInfo.Name);
                }
                await Task.Delay(throttling.Value);
            }
        }

        private async IAsyncEnumerable<Recommendation> GetRecommendationsStream(TinderClient client)
        {
            while (true)
            {
                foreach (var rec in await client.GetRecommendations())
                {
                    yield return rec;
                }
            }
        }
    }
}