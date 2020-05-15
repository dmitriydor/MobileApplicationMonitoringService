using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase db;
        public IMongoCollection<ApplicationData> Applications => db.GetCollection<ApplicationData>("Applications");
        public IMongoCollection<Event> Events => db.GetCollection<Event>("Events");
        public IMongoCollection<EventDescription> EventDescriptions => db.GetCollection<EventDescription>("Event-Descriptons");

        public DbContext(IOptions<MongoOptions> options)
        {
            db = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.Database);
        }
    }
}
