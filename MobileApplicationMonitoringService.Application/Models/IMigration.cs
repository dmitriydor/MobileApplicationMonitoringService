using System;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Models
{
    public interface IMigration
    {
        public Version Version { get; set; }
        public string Description { get; set; }
        public void Up(IMongoDatabase db);
    }
}