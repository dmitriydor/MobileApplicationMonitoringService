using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Migrations.Migrations
{
    class AddedPropertyCriticalityInEvents:IMigration
    {
        public Version Version { get; set; } = new Version(0, 0, 3);
        public string Description { get; set; } = "Added property Criticality in events";
        public void Up(IMongoDatabase db)
        {
            var update = Builders<Event>.Update.Set(e => e.Criticality, false);
            db.GetCollection<Event>("Events").UpdateMany(_ => true, update);
        }
    }
}
