using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IEventRepository
    {
        public Task<IEnumerable<Event>> GetAllForAsync(Guid identificationId);
        public Task CreateAsync(Event data);
        public Task UpdateAsync(Event data);
        public Task DeleteAsync(Guid id);
    }
}
