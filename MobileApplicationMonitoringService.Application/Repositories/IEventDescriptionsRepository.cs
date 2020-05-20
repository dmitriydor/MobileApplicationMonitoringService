using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IEventDescriptionsRepository
    {
        public Task<List<EventDescription>> GetAllEvensAsync();
        public Task<EventDescription> GetByEventNameAsync(string eventName);
        public Task UpdateBatchEventAsync(IEnumerable<EventDescription> eventDescriptions);
        public Task AddBatchEventAsync(IEnumerable<EventDescription> eventDescriptions);
    }
}
