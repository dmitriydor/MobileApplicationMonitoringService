﻿using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public class EventService : IEventService
    {
        private readonly UnitOfWorkFactory unitOfWorkFactory;
        public EventService(UnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }
        public async Task<List<EventDescription>> GetAllEventsAsync()
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var eventDescriptionsRepository = uow.GetRepository<EventDescriptionsRepository>();
            var eventDescriptions = await eventDescriptionsRepository.GetAllEvensAsync();
            uow.Commit();
            return eventDescriptions;
        }
        public async Task UpdateBatchEventAsync(IEnumerable<EventDescription> eventDescriptions)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var eventDescriptionsRepository = uow.GetRepository<EventDescriptionsRepository>();
            await eventDescriptionsRepository.UpdateBatchEventAsync(eventDescriptions);
            uow.Commit();
        }
    }
}
