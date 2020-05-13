using MongoDB.Driver;
using System;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    public interface IMigration
    {
        public Version Version { get; set; }
        public string Description { get; set; }
        public void Up(IMongoDatabase db);
    }
}