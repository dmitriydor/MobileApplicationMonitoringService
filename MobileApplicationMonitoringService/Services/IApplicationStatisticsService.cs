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
        Task<List<ApplicationResponse>> GetAllApplicationsAsync();
        Task<ApplicationStatisticsResponse> GetApplicationStatisticsByIdAsync(Guid id);
        Task DeleteEventsByApplicationIdAsync(Guid id);
        Task SendindNotificatins(SaveApplicationStatisticsRequest request);
    }
}
