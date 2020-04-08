using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Sevices
{
    public interface IEventService
    {
        Task<IdentificationData> CreateEventAndIdentificationData(CreateIdentificationDataRequest request);
    }
}
