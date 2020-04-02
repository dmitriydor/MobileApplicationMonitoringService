using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IIdentificationRepository
    {
        public Task<IEnumerable<IdentificationData>> GetAllAsync();
        public Task<IdentificationData> GetByIdAsync(Guid id);
        public Task<IdentificationData> CreateAsync(IdentificationData data);
        public Task<IdentificationData> UpdateAsync(IdentificationData data);
        public Task DeleteAsync(Guid id);
    }
}
