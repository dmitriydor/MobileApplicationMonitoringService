using MapsterMapper;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public class ApplicationStatisticService : IApplicationStatisticService
    {
        private readonly IApplicationDataRepository identificationRepository;
        private readonly IApplicationEventRepository eventRepository;
        private readonly IMapper mapper;
        public ApplicationStatisticService(IApplicationDataRepository identificationRepository, IApplicationEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.identificationRepository = identificationRepository;
            this.mapper = mapper;
        }
        public async Task<ApplicationDataResponse> SaveApplicationStatistic(CreateApplicationDataRequest request)
        {
            var device = mapper.Map<ApplicationData>(request);
            if(device != null)
            {
                await identificationRepository.UpsertAsync(device);
            }

            var events = request.Events;
            if(events != null)
            {
                events.ForEach(e => e.ApplicationId = device.Id);
                await eventRepository.CreateBatchAsync(events);
            }
            return mapper.Map<ApplicationDataResponse>(request);
        }
    }
}
