using Dan.Common.Extensions;
using Dan.Plugin.Skatteetaten.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Dan.Plugin.Skatteetaten
{
    class Program
    {
        private static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureDanPluginDefaults()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    // Add more configuration sources if necessary. ConfigureDanPluginDefaults will load environment variables, which includes
                    // local.settings.json (if developing locally) and applications settings for the Azure Function
                })
                .ConfigureServices((context, services) =>
                {
                    // Add any additional services here
                    services.AddLogging();

                    // This makes IOption<Settings> available in the DI container.
                    services.AddOptions<ApplicationSettings>()
                        .Configure<IConfiguration>((settings, configuration) => configuration.Bind(settings));
                    var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;
                })
                .Build();

            return host.RunAsync();
        }
    }
}

