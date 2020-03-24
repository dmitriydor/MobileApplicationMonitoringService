namespace MobileApplicationMonitoringService.Contracts.Requests
{
    public class UpdateIdentificationDataRequest
    {
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
    }
}