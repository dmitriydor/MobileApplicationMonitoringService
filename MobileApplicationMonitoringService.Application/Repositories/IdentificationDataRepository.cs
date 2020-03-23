using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class IdentificationDataRepository:IIdentificationDataRepository
    {
        private readonly List<IdentificationData> dataRepository = new List<IdentificationData>();
        public IEnumerable<IdentificationData> DataRepository => dataRepository;

        public IdentificationData Create(IdentificationData data)
        {
            dataRepository.Add(data);
            return data;
        }

        public IdentificationData Update(IdentificationData data)
        {
            var index = dataRepository.FindIndex(x => x == data);
            dataRepository[index] = data;
            return data;
        }

        public void Delete(IdentificationData data)
        {
            dataRepository.Remove(data);
        }
    }
}
