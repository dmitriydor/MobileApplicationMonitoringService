﻿using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Infrastructure;
using MobileApplicationMonitoringService.Services;
using Serilog;

namespace MobileApplicationMonitoringService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<IDbContext, DbContext>();
            services.AddSingleton<IMongoOptions, MongoOptions>();
            services.AddScoped<IApplicationDataRepository, ApplicationDataRepository>();
            services.AddScoped<IApplicationEventRepository, ApplicationEventRepository>();
            services.AddScoped<IApplicationStatisticService, ApplicationStatisticService>();
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
            
            MigrationRunner runner = new MigrationRunner(new MongoOptions(Configuration));
            try
            {
                runner.UpdateToLatestMigration();
            }
            catch (System.Exception e)
            {
                Log.Logger.Error(e.Message);
            }
            
            app.UseMiddleware<StatusCodeExceptionHandler>();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
