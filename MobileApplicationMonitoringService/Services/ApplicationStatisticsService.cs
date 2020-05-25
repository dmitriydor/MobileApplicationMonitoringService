using MapsterMapper;
using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Kafka;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public class ApplicationStatisticsService : IApplicationStatisticsService
    {
        private readonly IMapper mapper;
        private readonly UnitOfWorkFactory unitOfWorkFactory;
        private readonly Producer producer;
        public ApplicationStatisticsService(IMapper mapper, UnitOfWorkFactory unitOfWorkFactory, IOptions<KafkaOptions> kafkaOptions)
        {
            this.mapper = mapper;
            this.unitOfWorkFactory = unitOfWorkFactory;
            producer = new Producer(kafkaOptions.Value);
        }
        private async Task SaveEventDescriptins(UnitOfWork unitOfWork, SaveApplicationStatisticsRequest request)
        {
            var eventDescriptionsRepository = unitOfWork.GetRepository<EventDescriptionsRepository>();
            var eventNames = mapper.Map<List<EventDescription>>(request.Events).Select(x=>x.EventName).Distinct();
            if (eventNames != null)
            {
                var eventDescriptions = new List<EventDescription>();
                foreach(var eventName in eventNames)
                {
                    eventDescriptions.Add(new EventDescription { EventName = eventName });
                }
                await eventDescriptionsRepository.AddBatchEventAsync(eventDescriptions);
            }
        }
        private async Task SaveApplication(UnitOfWork unitOfWork, SaveApplicationStatisticsRequest request)
        {
            var applicationRepository = unitOfWork.GetRepository<ApplicationsRepository>();
            var applicationData = mapper.Map<ApplicationData>(request);
            if (applicationData != null)
            {
                await applicationRepository.UpsertAsync(applicationData);
            }
        }
        private async Task SaveEvents(UnitOfWork unitOfWork, SaveApplicationStatisticsRequest request)
        {
            var eventRepository = unitOfWork.GetRepository<EventsRepository>();
            List<Event> applicationEvents = mapper.Map<List<Event>>(request.Events);
            if (applicationEvents != null)
            {
                applicationEvents.ForEach(e => e.ApplicationId = request.Id);
                await eventRepository.CreateBatchAsync(applicationEvents);
            }
        }
        public async Task SaveApplicationStatisticsAsync(SaveApplicationStatisticsRequest request)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            await SaveEventDescriptins(uow, request);
            await SaveApplication(uow, request);
            await SaveEvents(uow, request);
            uow.Commit();
        }
        public async Task DeleteApplicationStatisticsAsync(Guid id)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationsRepository>();
            var eventRepository = uow.GetRepository<EventsRepository>();

            await eventRepository.DeleteAllForAsync(id);
            await applicationRepository.DeleteAsync(id);
            uow.Commit();
        }
        public async Task<List<ApplicationResponse>> GetAllApplicationsAsync()
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationsRepository>();

            var applicationData = await applicationRepository.GetAllAsync();
            if (applicationData == null)
            {
                return null;
            }
            var applicationStatistics = mapper.Map<List<ApplicationResponse>>(applicationData);
            uow.Commit();
            return applicationStatistics;
        }
        public async Task<ApplicationStatisticsResponse> GetApplicationStatisticsByIdAsync(Guid id)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var applicationRepository = uow.GetRepository<ApplicationsRepository>();
            var eventRepository = uow.GetRepository<EventsRepository>();
            
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
        public async Task DeleteEventsByApplicationIdAsync(Guid id)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var eventRepository = uow.GetRepository<EventsRepository>();
            await eventRepository.DeleteAllForAsync(id);
            uow.Commit();
        }
        public async Task SendindNotificatins(SaveApplicationStatisticsRequest request)
        {
            using var uow = unitOfWorkFactory.CreateUnitOfWork();
            var eventDescriptionsRepository = uow.GetRepository<EventDescriptionsRepository>();
            ApplicationData applicationData = mapper.Map<ApplicationData>(request);
            List<Event> applicationEvents = mapper.Map<List<Event>>(request.Events);
            var messages = new List<Message>();
            applicationEvents.ForEach(e =>
            {
                if (e.Criticality)
                    messages.Add(new Message
                    {
                        EventName = e.EventName,
                        Date = e.Date,
                        UserName = applicationData.UserName,
                        AppVersion = applicationData.AppVersion,
                        EventDescription = eventDescriptionsRepository.GetByEventNameAsync(e.EventName).Result.Description
                    });
            });
            uow.Commit();
            await producer.Produce(messages.ToArray(), "notification");
        }
    }
}
