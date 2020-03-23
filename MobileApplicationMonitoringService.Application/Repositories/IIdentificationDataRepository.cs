using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public interface IIdentificationDataRepository
    {
        public IEnumerable<IdentificationData> DataRepository { get;}
        public IdentificationData Create(IdentificationData data);
        public IdentificationData Update(IdentificationData data);
        public void Delete(IdentificationData data);
    }
}
