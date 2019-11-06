using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tinder.AutoMatcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var tinderAuthToken = hostContext.Configuration.GetValue<string>("TinderClient:Token"); // X-Auth-Token
            if (string.IsNullOrEmpty(tinderAuthToken) || !Guid.TryParse(tinderAuthToken, out Guid tinderAuthTokenGuid))
            {
                throw new Exception("TinderClient:Token is missing in appsettings.json or the token is malformed");
            }

            var tinderClient = new TinderClient(tinderAuthTokenGuid);

            services.AddSingleton<ITinderClient>(tinderClient);
            services.AddHostedService<Worker>();
        }
    }
}
