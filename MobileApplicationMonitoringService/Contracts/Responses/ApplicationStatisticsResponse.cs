using MobileApplicationMonitoringService.Application.Models;
using System;
using System.Collections.Generic;

namespace MobileApplicationMonitoringService.Contracts.Responses
{
    public class ApplicationStatisticsResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public List<ApplicationEvent> Events { get; set; }
    }
}
