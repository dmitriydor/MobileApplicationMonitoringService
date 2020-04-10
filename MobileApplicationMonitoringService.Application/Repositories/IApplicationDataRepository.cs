using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IApplicationDataRepository
    {
        public Task<List<ApplicationData>> GetAllAsync();
        public Task<ApplicationData> GetByIdAsync(Guid id);
        public Task<ApplicationData> UpsertAsync(ApplicationData data);
        public Task<ApplicationData> UpdateAsync(ApplicationData data);
        public Task DeleteAsync(Guid id);
    }
}
