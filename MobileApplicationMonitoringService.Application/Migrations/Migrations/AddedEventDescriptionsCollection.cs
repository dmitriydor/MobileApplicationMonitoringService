using MongoDB.Driver;
using System;

namespace MobileApplicationMonitoringService.Application.Migrations.Migrations
{
    class AddedEventDescriptionsCollection : IMigration
    {
        public Version Version { get; set; } = new Version(0, 0, 2);
        public string Description { get; set; } = "Added EventDescriptions collection";
        public void Up(IMongoDatabase db)
        {
            db.CreateCollection("EventDescriptons");
        }
    }
}
