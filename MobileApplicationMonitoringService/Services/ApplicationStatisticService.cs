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
        private readonly IApplicationDataRepository applicationRepository;
        private readonly IApplicationEventRepository eventRepository;
        private readonly IMapper mapper;
        public ApplicationStatisticService(IApplicationDataRepository applicationRepository, IApplicationEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }
        public async Task<ApplicationDataResponse> SaveApplicationStatisticAsync(CreateApplicationDataRequest request)
        {
            var device = mapper.Map<ApplicationData>(request);
            if(device != null)
            {
                await applicationRepository.UpsertAsync(device);
            }

            var events = request.Events;
            if(events != null)
            {
                events.ForEach(e => e.ApplicationId = device.Id);
                await eventRepository.CreateBatchAsync(events);
            }
            return mapper.Map<ApplicationDataResponse>(request);
        }
        public async Task DeleteApplicationStatisticsAsync(Guid id)
        {
            await eventRepository.DeleteAllForAsync(id);
            await applicationRepository.DeleteAsync(id);
        }
    }
}
