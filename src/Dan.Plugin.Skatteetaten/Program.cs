using Dan.Common.Extensions;
using Dan.Plugin.Skatteetaten.Config;
using Dan.Plugin.Skatteetaten.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTelemetry.Trace;
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
                .ConfigureLogging(logging =>
                {
                    // The HttpClient factory logs "Start processing HTTP request GET <url>" at Information level. The Freg and
                    // SummertSkattegrunnlag URLs contain the subject's national identity number, so keep these lines out of the logs.
                    logging.AddFilter("System.Net.Http.HttpClient", LogLevel.Warning);
                })
                .ConfigureServices((context, services) =>
                {
                    // Add any additional services here
                    services.AddLogging();

                    // This makes IOption<Settings> available in the DI container.
                    services.AddOptions<ApplicationSettings>()
                        .Configure<IConfiguration>((settings, configuration) => configuration.Bind(settings));
                    var applicationSettings = services.BuildServiceProvider().GetRequiredService<IOptions<ApplicationSettings>>().Value;

                    // Mask national identity numbers in outgoing HTTP dependency spans before they are exported to Azure Monitor.
                    services.ConfigureOpenTelemetryTracerProvider(builder => builder.AddProcessor<SsnRedactingProcessor>());
                })
                .Build();

            return host.RunAsync();
        }
    }
}
