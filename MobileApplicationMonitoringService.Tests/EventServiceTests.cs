using FluentAssertions;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MobileApplicationMonitoringService.Tests
{
    public class EventServiceTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> factory;
        public EventServiceTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        [Fact]
        public async void GetAllEvents_EmptyDB_ShouldBeNullOrEmpty()
        {
            var eventService = (IEventService)factory.Services.GetService(typeof(IEventService));
            var result = await eventService.GetAllEventsAsync();
            result.Should().BeNullOrEmpty();
        }
        [Fact]
        public async void GetAllEvents_ValidModel_ShouldGetCollectionEventDescriptions()
        {
            Guid id = Guid.NewGuid();
            var request = new SaveApplicationStatisticsRequest()
            {
                Id = id,
                AppVersion = "0.0.0",
                OperationSystem = "windows",
                UserName = "Dmitriy Doronin",
                Events = new List<SaveApplicationStatisticsRequest.Event>
                {
                    new SaveApplicationStatisticsRequest.Event()
                    {
                        Date = DateTime.UtcNow,
                        EventName = "Start"
                    },
                    new SaveApplicationStatisticsRequest.Event()
                    {
                        Date = DateTime.UtcNow,
                        EventName = "Stop"
                    }
                }
            };
            var applicationStatisticsService =
                (IApplicationStatisticsService)factory.Services.GetService(typeof(IApplicationStatisticsService));
            var eventService = (IEventService)factory.Services.GetService(typeof(IEventService));
            await applicationStatisticsService.SaveApplicationStatisticsAsync(request);
            var result = await eventService.GetAllEventsAsync();
            result.Should().BeOfType<List<EventDescription>>();
        }
        [Fact]
        public async void UpdateBatchEvent_ValidModel_ShouldUpsertEventDescriptions()
        {
            Guid id = Guid.NewGuid();
            var request = new SaveApplicationStatisticsRequest()
            {
                Id = id,
                AppVersion = "0.0.0",
                OperationSystem = "windows",
                UserName = "Dmitriy Doronin",
                Events = new List<SaveApplicationStatisticsRequest.Event>
                {
                    new SaveApplicationStatisticsRequest.Event()
                    {
                        Date = DateTime.UtcNow,
                        EventName = "Start_App"
                    },
                    new SaveApplicationStatisticsRequest.Event()
                    {
                        Date = DateTime.UtcNow,
                        EventName = "Stop_App"
                    }
                }
            };
            var eventDescriptions = new List<EventDescription> {
                new EventDescription
                {
                    Description = "Старт приложения",
                    EventName = "Start_App"
                },
                new EventDescription
                {
                    Description = "Остановка приложения",
                    EventName = "Stop_App"
                }
            };
            var applicationStatisticsService =
                (IApplicationStatisticsService)factory.Services.GetService(typeof(IApplicationStatisticsService));
            var eventService = (IEventService)factory.Services.GetService(typeof(IEventService));
            await applicationStatisticsService.SaveApplicationStatisticsAsync(request);
            await eventService.UpdateBatchEventAsync(eventDescriptions);
            var result = await eventService.GetAllEventsAsync();
            result.Should().NotBeNullOrEmpty().And.AllBeOfType(typeof(EventDescription)).And.HaveCount(2);
        }
    }
}
