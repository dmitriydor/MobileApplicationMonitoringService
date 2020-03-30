using System;
using System.Collections.Generic;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class FakeRepository:IIdentificationDataRepository
    {
        private readonly Dictionary<Guid, IdentificationData> dataRepository = new Dictionary<Guid, IdentificationData>()
        {
            [new Guid("682c3220-be4b-4c07-aeca-8b6473d5a85f")] = new IdentificationData() { Id = new Guid("682c3220-be4b-4c07-aeca-8b6473d5a85f"), AppVersion = "2.2.4", OperationSystem = "android", UserName = "dmitriy doronin", Date = DateTime.UtcNow },
            [new Guid("74e3fc74-f45c-41e1-989c-b7c53acd4003")] = new IdentificationData() { Id = new Guid("74e3fc74-f45c-41e1-989c-b7c53acd4003"), AppVersion = "1.4.0", OperationSystem = "windows", UserName = "dmitriy doronin", Date = DateTime.UtcNow },
            [new Guid("64a1dc26-bf0e-47e1-aeb6-d5bea2faacdf")] = new IdentificationData() { Id = new Guid("64a1dc26-bf0e-47e1-aeb6-d5bea2faacdf"), AppVersion = "2.2.4", OperationSystem = "android", UserName = "dmitriy doronin", Date = DateTime.UtcNow },
            [new Guid("bdc14dc1-29f1-4a0c-b376-0e01d4400396")] = new IdentificationData() { Id = new Guid("bdc14dc1-29f1-4a0c-b376-0e01d4400396"), AppVersion = "2.2.4", OperationSystem = "android", UserName = "dmitriy doronin", Date = DateTime.UtcNow },

        };

        public Dictionary<Guid, IdentificationData> GetAll()
        {
            var clone = new Dictionary<Guid, IdentificationData>();
            foreach (KeyValuePair<Guid, IdentificationData> identificationData in dataRepository)
            {
                clone.Add(identificationData.Key, identificationData.Value);
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
            data.Date = DateTime.UtcNow;
            dataRepository.Add(id, data);
            return data;
        }

        public IdentificationData Update(IdentificationData data)
        {
            if (!dataRepository.ContainsKey(data.Id))
            {
                return null;
            }
            data.Date = DateTime.UtcNow;
            dataRepository[data.Id] = data;
            return dataRepository[data.Id];
        }

        public void Delete(Guid id)
        {
            dataRepository.Remove(id);
        }
    }
}