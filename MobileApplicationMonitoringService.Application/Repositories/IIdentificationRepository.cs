using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IIdentificationRepository
    {
        public Task<IEnumerable<IdentificationData>> GetAll();
        public Task<IdentificationData> GetById(Guid id);
        public Task<IdentificationData> Create(IdentificationData data);
        public Task<IdentificationData> Update(IdentificationData data);
        public Task Delete(Guid id);
    }
}
