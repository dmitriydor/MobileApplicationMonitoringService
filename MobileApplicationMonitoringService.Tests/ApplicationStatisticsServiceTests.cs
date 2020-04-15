using MapsterMapper;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Services;
using System;
using System.Collections.Generic;
using MobileApplicationMonitoringService.Contracts.Requests;
using Xunit;

namespace MobileApplicationMonitoringService.Tests
{
    [Collection("Database context")]
    public class ApplicationStatisticsServiceTests
    {
        private DbContextFixture fixture;

        public ApplicationStatisticsServiceTests(DbContextFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async void  SaveApplicationStatistics_CanSaveModel_Test()
        {
            Guid id = Guid.NewGuid();
            var request = new SaveApplicationStatisticsRequest() {
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
            var mockAppDataRep = new ApplicationDataRepository(fixture);
            var mockAppEventRep = new ApplicationEventRepository(fixture);
            var mockMapper = new Mapper();

            var applicationStatisticsService =
                new ApplicationStatisticsService(mockAppDataRep, mockAppEventRep, mockMapper);
            await applicationStatisticsService.SaveApplicationStatisticsAsync(request);
            var result = 
                await applicationStatisticsService.GetApplicationStatisticsByIdAsync(id);
            Assert.NotNull(result);
        }
    }
}