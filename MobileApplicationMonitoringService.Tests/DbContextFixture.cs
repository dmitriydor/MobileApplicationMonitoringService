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
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<Event> Events => db.GetCollection<Event>("Events");
        public IMongoCollection<EventDescription> EventDescriptions => db.GetCollection<EventDescription>("EventDescriptons");

        public DbContextFixture()
        {
            dbClient = new MongoClient("mongodb://localhost:27017");
            db = dbClient.GetDatabase("test");
        }
        public void Dispose()
        {
            dbClient.DropDatabase("test");
        }

    }
}