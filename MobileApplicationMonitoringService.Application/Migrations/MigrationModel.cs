using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    public class MigrationModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Version Version { get; set; } = new Version(0, 0, 0);
        public string Description { get; set; } = "Default";
    }
}
