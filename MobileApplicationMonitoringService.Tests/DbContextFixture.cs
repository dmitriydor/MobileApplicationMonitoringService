using System;
using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Tests
{
    public class DbContextFixture : IDisposable,IDbContext
    {
        private const string dbName = "testdb";
        private readonly IMongoClient dbClient;
        private readonly IMongoDatabase db;
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<ApplicationEvent> Events => db.GetCollection<ApplicationEvent>("Events");

        public DbContextFixture()
        { 
            dbClient = new MongoClient("mongodb://admin:admin@localhost:27017");
            db = dbClient.GetDatabase(dbName);
        }
        public void Dispose()
        {
            dbClient.DropDatabase(dbName);
        }
        
    }
}