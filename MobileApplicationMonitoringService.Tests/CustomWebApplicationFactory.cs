using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Options;
using System.Linq;
using Xunit;

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
                services.Configure<MongoOptions>(options =>
                {
                    options.ConnectionString = "mongodb://localhost:27017";
                    options.Database = "test";
                });
            });
        }
    }
}
