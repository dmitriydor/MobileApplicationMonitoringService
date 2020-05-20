using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IEventsRepository
    {
        public Task<List<EventWithDescription>> GetAllForAsync(Guid applicationId);
        public Task CreateBatchAsync(IEnumerable<Event> events);
        public Task CreateAsync(Event data);
        public Task DeleteAllForAsync(Guid applicationId);
    }
}
