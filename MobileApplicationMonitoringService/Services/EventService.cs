using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public class EventService : IEventService
    {
        public async Task<List<EventDescription>> GetAllEventsAsync()
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var eventDescriptionsRepository = uow.GetRepository<EventDescriptionsRepository>();
            var eventDescriptions = await eventDescriptionsRepository.GetAllEvensAsync();
            uow.Commit();
            return eventDescriptions;
        }
        public async Task UpdateBatchEventAsync(IEnumerable<EventDescription> eventDescriptions)
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var eventDescriptionsRepository = uow.GetRepository<EventDescriptionsRepository>();
            await eventDescriptionsRepository.UpdateBatchEventAsync(eventDescriptions);
            uow.Commit();
        }
    }
}
