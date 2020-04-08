using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase db;
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<ApplicationEvent> Events => db.GetCollection<ApplicationEvent>("Events");
        public DbContext(IMongoOptions options)
        {
            db = new MongoClient(options.ConnectionString).GetDatabase(options.Database);
        }
    }
}
