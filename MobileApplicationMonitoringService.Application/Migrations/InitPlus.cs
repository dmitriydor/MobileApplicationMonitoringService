using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    class InitPlus : IMigration
    {
        public Version Version { get; set; } = new Version(0, 0, 2);
        public string Description { get; set; } = "Initial migration 2";
        public void Up(IMongoDatabase db)
        {
        }
    }
}
