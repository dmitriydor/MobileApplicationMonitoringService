using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Migrations;
using MobileApplicationMonitoringService.Application.Options;
using MobileApplicationMonitoringService.Infrastructure;
using MobileApplicationMonitoringService.Services;
using MongoDB.Driver;
using Serilog;

namespace MobileApplicationMonitoringService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoOptions>(Configuration.GetSection("MongoOptions"));
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<IDbContext, DbContext>();
            services.AddSingleton<MigrationRunner>();
            services.AddSingleton<IMongoDatabase>(serviceProvider =>
            {
                var mongoOptions = serviceProvider.GetService<IOptions<MongoOptions>>();
                return new MongoClient(mongoOptions.Value.ConnectionString).GetDatabase(mongoOptions.Value.Database);
            });
            services.AddSingleton<UnitOfWorkFactory>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IApplicationStatisticsService, ApplicationStatisticsService>();
            services.AddSwaggerDocument(option =>
            {
                option.Title = "Mobile application monitoring service API";
                option.Description = "";
                option.Version = "v1";
            });
            services.AddCors();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var runner = app.ApplicationServices.GetService<MigrationRunner>();
            try
            {
                runner.UpdateToLatestMigration();
            }
            catch (System.Exception e)
            {
                Log.Logger.Error(e.Message);
                throw e;
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<StatusCodeExceptionHandler>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
