using System.Collections.Generic;
using System.Linq;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class FakeRepository:IIdentificationDataRepository
    {
        private readonly List<IdentificationData> dataRepository = new List<IdentificationData>()
        {
            new IdentificationData(){Id = 1,AppVersion = "2.2.4", OS = "android", UserName = "Dmitriy Doroinin"},
            new IdentificationData(){Id = 2,AppVersion = "1.0.4", OS = "windows", UserName = "Kan Utou"},
            new IdentificationData(){Id = 3,AppVersion = "1.0.4", OS = "windows", UserName = "Vadim Rutin"},
            new IdentificationData(){Id = 4,AppVersion = "2.2.4", OS = "android", UserName = "Evgen Umatonlov"},
            new IdentificationData(){Id = 5,AppVersion = "1.0.4", OS = "windows", UserName = "Anton Antonov"},
        };

        public IEnumerable<IdentificationData> DataRepository => dataRepository;

        public IdentificationData Create(IdentificationData data)
        {
            dataRepository.Add(data);
            return data;
        }

        public IdentificationData Update(IdentificationData data)
        {
            var index = dataRepository.FindIndex(d => d == data);
            dataRepository[index] = data;
            return data;
        }

        public void Delete(IdentificationData data)
        {
            dataRepository.Remove(data);
        }
    }
}