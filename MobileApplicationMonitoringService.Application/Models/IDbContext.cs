using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public interface IDbContext
    {
        IMongoCollection<ApplicationData> Applications { get; }
        IMongoCollection<ApplicationEvent> Events { get; }
    }
}
