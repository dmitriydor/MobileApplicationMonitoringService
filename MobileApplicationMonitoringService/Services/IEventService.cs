using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public interface IEventService
    {
        public Task<List<EventDescription>> GetAllEventsAsync();
        public Task AddEventAsync(EventDescription eventDescription);
        public Task UpdateEventAsync(EventDescription eventDescription);
    }
}
