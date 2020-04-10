using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Data
{
    public interface IDbContext
    {
        IMongoCollection<ApplicationData> Applications { get; }
        IMongoCollection<ApplicationEvent> Events { get; }
    }
}
