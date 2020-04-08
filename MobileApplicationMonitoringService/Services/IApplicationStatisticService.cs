using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Contracts.Requests;
using MobileApplicationMonitoringService.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Services
{
    public interface IApplicationStatisticService
    {
        Task<ApplicationDataResponse> SaveApplicationStatistic(CreateApplicationDataRequest request);

    }
}
