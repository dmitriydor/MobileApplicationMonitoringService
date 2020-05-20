using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    public class Init : IMigration
    {
        public Version Version { get; set; } = new Version(0, 0, 1);
        public string Description { get; set; } = "Initial migration";
        public void Up(IMongoDatabase db)
        {
            db.CreateCollection("Events");
            db.CreateCollection("Applications");

            var keyIdentificationId = Builders<Event>.IndexKeys.Ascending("ApplicationId");
            var keyDate = Builders<Event>.IndexKeys.Ascending("Date");
            db.GetCollection<Event>("Events").Indexes.CreateMany(new[] {
                new CreateIndexModel<Event>(keyIdentificationId),
                new CreateIndexModel<Event>(keyDate)
            });
        }
    }
}