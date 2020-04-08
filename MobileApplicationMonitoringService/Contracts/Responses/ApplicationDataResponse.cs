using System;
using System.Collections.Generic;
using MobileApplicationMonitoringService.Application.Models;

namespace MobileApplicationMonitoringService.Contracts.Responses
{
    public class ApplicationDataResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public List<ApplicationEvent> Events { get; set; }
    }
}