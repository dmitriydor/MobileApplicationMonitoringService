using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase db;
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<ApplicationEvent> Events => db.GetCollection<ApplicationEvent>("Events");
        public DbContext(IOptions<MongoOptions> options)
        {
            db = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.Database);
        }
    }
}
