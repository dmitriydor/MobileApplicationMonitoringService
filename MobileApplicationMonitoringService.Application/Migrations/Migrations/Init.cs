﻿using System;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    public class Init:IMigration
    {
        public Version Version { get; set; } = new Version(0,0,1);
        public string Description { get; set; } = "Initial migration";
        public void Up(IMongoDatabase db)
        {
            db.CreateCollection("Events");
            db.CreateCollection("Applications");

            var keyIdentificationId = Builders<ApplicationEvent>.IndexKeys.Ascending("ApplicationId");
            var keyDate = Builders<ApplicationEvent>.IndexKeys.Ascending("Date");
            db.GetCollection<ApplicationEvent>("Events").Indexes.CreateMany(new[] {
                new CreateIndexModel<ApplicationEvent>(keyIdentificationId),
                new CreateIndexModel<ApplicationEvent>(keyDate) 
            });
        }
    }
}