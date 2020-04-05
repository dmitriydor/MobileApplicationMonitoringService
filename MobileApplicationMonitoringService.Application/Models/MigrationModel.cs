using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class MigrationModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Version Version { get; set; } = new Version(0, 0, 0);
        public string Description { get; set; } = "Default";
    }
}
