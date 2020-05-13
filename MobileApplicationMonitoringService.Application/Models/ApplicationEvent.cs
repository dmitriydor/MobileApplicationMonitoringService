using System;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class ApplicationEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
