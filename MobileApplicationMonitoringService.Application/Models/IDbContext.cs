using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public interface IDbContext
    {
        IMongoCollection<IdentificationData> IdentificationList { get; }
        IMongoCollection<Event> Events { get; }
    }
}
