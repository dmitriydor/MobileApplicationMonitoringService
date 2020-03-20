using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class IdentificationDataRepository:IIdentificationDataRepository
    {
        private List<IdentificationData> dataRepository = new List<IdentificationData>();
        public IEnumerable<IdentificationData> DataRepository 
        {
            get
            {
                return dataRepository;
            }
        }

        public void Create(IdentificationData data)
        {
            dataRepository.Add(data);
        }

        public void Update(IdentificationData data)
        {
            var index = dataRepository.FindIndex(x => x == data);
            dataRepository[index] = data;
        }

        public void Delete(IdentificationData data)
        {
            dataRepository.Remove(data);
        }
    }
}
