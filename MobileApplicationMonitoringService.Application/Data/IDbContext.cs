using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Data
{
    public interface IDbContext
    {
        IMongoCollection<ApplicationData> Applications { get; }
        IMongoCollection<ApplicationEvent> Events { get; }
    }
}
