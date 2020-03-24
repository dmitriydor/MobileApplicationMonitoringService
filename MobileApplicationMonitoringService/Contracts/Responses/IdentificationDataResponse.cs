using System;

namespace MobileApplicationMonitoringService.Contracts.Responses
{
    public class IdentificationDataResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
    }
}