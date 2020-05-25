using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileApplicationMonitoringService.Application.Options;
using NotificationService.Options;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
            .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json"))
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();
                services.Configure<KafkaOptions>(hostContext.Configuration.GetSection("Kafka"));
                services.Configure<EmailOptions>(hostContext.Configuration.GetSection("EmailOptions"));
                services.AddHostedService<NotificationSandingService>();
            });
    }
}
