using MapsterMapper;
using MobileApplicationMonitoringService.Application.Repositories;
using MobileApplicationMonitoringService.Services;
using System;
using System.Collections.Generic;
using FluentAssertions;
using MobileApplicationMonitoringService.Application.Migrations;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
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
        public async void  SaveApplicationStatistics_ValidModel_ShouldSaveModel()
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
            result.Should().BeOfType<ApplicationStatisticsResponse>().Which.Id.Should().Be(id);
        }
        [Fact]
        public async void DeleteApplicationStatistics_ValidModel_ShouldDeleteModel()
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
            await applicationStatisticsService.DeleteApplicationStatisticsAsync(id);
            var result =
                await applicationStatisticsService.GetApplicationStatisticsByIdAsync(id);
            result.Should().BeNull();
        }    
    }
}