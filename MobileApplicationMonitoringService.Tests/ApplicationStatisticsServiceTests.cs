using FluentAssertions;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using MobileApplicationMonitoringService.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MobileApplicationMonitoringService.Tests
{
    public class ApplicationStatisticsServiceTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private CustomWebApplicationFactory<Startup> factory;
        public ApplicationStatisticsServiceTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        [Fact]
        public async void SaveApplicationStatistics_ValidModel_ShouldSaveModel()
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
            var applicationStatisticsService = (IApplicationStatisticsService)factory.Services.GetService(typeof(IApplicationStatisticsService));
            await applicationStatisticsService.SaveApplicationStatisticsAsync(request);
            var result =
                await applicationStatisticsService.GetApplicationStatisticsByIdAsync(id);
            result.Should().BeOfType<ApplicationStatisticsResponse>().Which.Id.Should().Be(id);
        }
        [Fact]
        public async void DeleteApplicationStatistics_ValidModel_ShouldDeleteModel()
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
            var applicationStatisticsService = (IApplicationStatisticsService)factory.Services.GetService(typeof(IApplicationStatisticsService));
            await applicationStatisticsService.SaveApplicationStatisticsAsync(request);
            await applicationStatisticsService.DeleteApplicationStatisticsAsync(id);
            var result =
                await applicationStatisticsService.GetApplicationStatisticsByIdAsync(id);
            result.Should().BeNull();
        }
    }
}