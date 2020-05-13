using MapsterMapper;
using MobileApplicationMonitoringService.Application.Data;
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
        private readonly IMapper mapper;
        public ApplicationStatisticsService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task SaveApplicationStatisticsAsync(SaveApplicationStatisticsRequest request)
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();

            var applicationRepository = uow.GetRepository<ApplicationDataRepository>();
            var eventRepository = uow.GetRepository<ApplicationEventRepository>();

            var applicationData = mapper.Map<ApplicationData>(request);
            if (applicationData != null)
            {
                await applicationRepository.UpsertAsync(applicationData);
            }
            List<ApplicationEvent> applicationEvents = mapper.Map<List<ApplicationEvent>>(request.Events);
            if (applicationEvents != null)
            {
                applicationEvents.ForEach(e => e.ApplicationId = applicationData.Id);
                await eventRepository.CreateBatchAsync(applicationEvents);
            }
            uow.Commit();
        }

        public async Task DeleteApplicationStatisticsAsync(Guid id)
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationDataRepository>();
            var eventRepository = uow.GetRepository<ApplicationEventRepository>();

            await eventRepository.DeleteAllForAsync(id);
            await applicationRepository.DeleteAsync(id);
            uow.Commit();
        }

        public async Task<List<ApplicationStatisticsResponse>> GetAllApplicationStatisticsAsync()
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationDataRepository>();
            var eventRepository = uow.GetRepository<ApplicationEventRepository>();

            var applicationData = await applicationRepository.GetAllAsync();
            if (applicationData == null)
            {
                return null;
            }

            var applicationStatistics = mapper.Map<List<ApplicationStatisticsResponse>>(applicationData);
            foreach (var app in applicationStatistics)
            {
                app.Events = await eventRepository.GetAllForAsync(app.Id);
            }
            uow.Commit();
            return applicationStatistics;
        }

        public async Task<ApplicationStatisticsResponse> GetApplicationStatisticsByIdAsync(Guid id)
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationDataRepository>();
            var eventRepository = uow.GetRepository<ApplicationEventRepository>();
            var applicationData = await applicationRepository.GetByIdAsync(id);
            if (applicationData == null)
            {
                return null;
            }
            var applicationStatistics = mapper.Map<ApplicationStatisticsResponse>(applicationData);
            applicationStatistics.Events = await eventRepository.GetAllForAsync(id);
            uow.Commit();
            return applicationStatistics;
        }

        public async Task DeleteEventsByApplicationId(Guid id)
        {
            using var uow = UnitOfWorkFactory.CreateUnitOfWork();
            var eventRepository = uow.GetRepository<ApplicationEventRepository>();
            await eventRepository.DeleteAllForAsync(id);
            uow.Commit();
        }
    }
}
