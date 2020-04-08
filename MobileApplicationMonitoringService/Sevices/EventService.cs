using MapsterMapper;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Sevices
{
    public class EventService : IEventService
    {
        private readonly IIdentificationRepository identificationRepository;
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;
        public EventService(IIdentificationRepository identificationRepository, IEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.identificationRepository = identificationRepository;
            this.mapper = mapper;
        }
        public async Task<IdentificationData> CreateEventAndIdentificationData(CreateIdentificationDataRequest request)
        {
            var identification = mapper.Map<IdentificationData>(request);
            if(identification != null)
            {
                await identificationRepository.CreateAsync(identification);
            }

            var events = request.Events;
            if(events != null)
            {
                foreach(var e in events)
                {
                    e.IdentificationId = identification.Id;
                    await eventRepository.CreateAsync(e);
                }
            }
            return identification;
        }
    }
}
