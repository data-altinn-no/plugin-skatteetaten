using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Dan.Common.Extensions;
using Dan.Plugin.Skatteetaten.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

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

                    // See https://docs.microsoft.com/en-us/azure/azure-monitor/app/worker-service#using-application-insights-sdk-for-worker-services
                    services.AddApplicationInsightsTelemetryWorkerService();

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

