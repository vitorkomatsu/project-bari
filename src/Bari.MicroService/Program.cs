using System.Threading.Tasks;
using Barigui.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Barigui.MicroService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder();

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            });

            builder.ConfigureServices((hostingContext, services) =>
            {
                services.RegisterServices(hostingContext.Configuration);
                services.AddSingleton(s => new Settings(hostingContext.Configuration));
                services.AddSingleton<IHostedService, HostedService>();
            });

            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddDebug();
                logging.AddConsole();
            });

            await builder.RunConsoleAsync();
        }
    }
}
