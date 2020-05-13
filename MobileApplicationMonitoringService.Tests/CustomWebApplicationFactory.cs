using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MobileApplicationMonitoringService.Application.Data;
using System.Linq;

namespace MobileApplicationMonitoringService.Tests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContext));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddScoped<IDbContext, DbContextFixture>();
            });
        }
    }
}
