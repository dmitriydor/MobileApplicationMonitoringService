using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IApplicationEventRepository
    {
        public Task<IEnumerable<ApplicationEvent>> GetAllForAsync(Guid applicationId);
        public Task CreateBatchAsync(IEnumerable<ApplicationEvent> events);
        public Task CreateAsync(ApplicationEvent data);
        public Task UpdateAsync(ApplicationEvent data);
        public Task DeleteAllForAsync(Guid applicationId);
        public Task DeleteAsync(Guid id);
    }
}
