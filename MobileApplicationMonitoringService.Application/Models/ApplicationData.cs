using System;

namespace MobileApplicationMonitoringService.Application.Models
{
    public sealed class ApplicationData
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
    }
}
