using System.Collections.Generic;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Contracts.Requests
{
    public class CreateIdentificationDataRequest
    {
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public List<Event> Events { get; set; }

    }
}