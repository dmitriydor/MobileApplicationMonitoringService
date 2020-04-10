using MapsterMapper;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public class ApplicationStatisticsService : IApplicationStatisticsService
    {
        private readonly IApplicationDataRepository applicationRepository;
        private readonly IApplicationEventRepository eventRepository;
        private readonly IMapper mapper;
        public ApplicationStatisticsService(IApplicationDataRepository applicationRepository, IApplicationEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }
        public async Task SaveApplicationStatisticsAsync(SaveApplicationStatisticsRequest request)
        {
            var applicationData = mapper.Map<ApplicationData>(request);
            if(applicationData != null)
            {
                await applicationRepository.UpsertAsync(applicationData);
            }
            List<ApplicationEvent> applicationEvents = mapper.Map<List<ApplicationEvent>>(request.Events);
            if(applicationEvents != null)
            {
                
                applicationEvents.ForEach(e => e.ApplicationId = applicationData.Id);
                await eventRepository.CreateBatchAsync(applicationEvents);
            }
        }

        public async Task DeleteApplicationStatisticsAsync(Guid id)
        {
            await eventRepository.DeleteAllForAsync(id);
            await applicationRepository.DeleteAsync(id);
        }

        public async Task<List<ApplicationStatisticsResponse>> GetAllApplicationStatisticsAsync()
        {
            var applicationData = await applicationRepository.GetAllAsync();
            var applicationStatistics = mapper.Map<List<ApplicationStatisticsResponse>>(applicationData);
            foreach(var app in applicationStatistics)
            {
                app.Events = await eventRepository.GetAllForAsync(app.Id);
            }
            return applicationStatistics;
        }

        public async Task<ApplicationStatisticsResponse> GetApplicationStatisticsByIdAsync(Guid id)
        {
            var applicationData = await applicationRepository.GetByIdAsync(id);
            var applicationStatistics = mapper.Map<ApplicationStatisticsResponse>(applicationData);
            applicationStatistics.Events = await eventRepository.GetAllForAsync(id);
            return applicationStatistics;
        }
    }
}
