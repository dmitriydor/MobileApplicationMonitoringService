using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public interface IApplicationStatisticsService
    {
        Task SaveApplicationStatisticsAsync(SaveApplicationStatisticsRequest request);
        Task DeleteApplicationStatisticsAsync(Guid id);
        Task<List<ApplicationStatisticsResponse>> GetAllApplicationStatisticsAsync();
        Task<ApplicationStatisticsResponse> GetApplicationStatisticsByIdAsync(Guid id);
    }
}
