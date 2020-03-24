using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IIdentificationDataRepository
    {
        public Dictionary<Guid, IdentificationData> GetAll();
        public IdentificationData GetById(Guid id);
        public IdentificationData Create(IdentificationData data);
        public IdentificationData Update(IdentificationData data);
        public void Delete(Guid id);
    }
}
