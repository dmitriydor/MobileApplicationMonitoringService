using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class IdentificationDataRepository:IIdentificationDataRepository
    {
        private readonly Dictionary<Guid,IdentificationData> dataRepository = new Dictionary<Guid, IdentificationData>();

        public Dictionary<Guid,IdentificationData> GetAll()
        {
            var clone = new Dictionary<Guid, IdentificationData>();
            foreach (KeyValuePair<Guid, IdentificationData> identificationData in clone)
            {
                clone.Add(identificationData.Key,identificationData.Value);
            }

            return clone;
        }

        public IdentificationData GetById(Guid id)
        {
            if (!dataRepository.ContainsKey(id))
            {
                return null;
            }

            return dataRepository[id];
        }
        public IdentificationData Create(IdentificationData data)
        {
            var id = Guid.NewGuid();
            data.Id = id;
            dataRepository.Add(id,data);
            return data;
        }

        public IdentificationData Update(Guid id,IdentificationData data)
        {
            if (!dataRepository.ContainsKey(id))
            {
                return null;
            }
            dataRepository[id] = data;
            return dataRepository[id];
        }

        public void Delete(Guid id)
        {
            dataRepository.Remove(id);
        }
    }
}
