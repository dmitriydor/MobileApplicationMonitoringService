using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Migrations;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;

namespace MobileApplicationMonitoringService.Tests
{
    public class DbContextFixture : IDisposable, IDbContext
    {
        private MongoOptions options = new MongoOptions();
        private readonly IMongoClient dbClient;
        private readonly IMongoDatabase db;
        private readonly MigrationRunner runner;
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<ApplicationEvent> Events => db.GetCollection<ApplicationEvent>("Events");

        public DbContextFixture()
        {
            options.Database = "monitoringdb";
            options.ConnectionString = "mongodb://localhost:27017";
            runner = new MigrationRunner(new OptionsWrapper<MongoOptions>(options));
            runner.UpdateToLatestMigration();
            dbClient = new MongoClient(options.ConnectionString);
            db = dbClient.GetDatabase(options.Database);
        }
        public void Dispose()
        {
            dbClient.DropDatabase(options.Database);
        }

    }
}