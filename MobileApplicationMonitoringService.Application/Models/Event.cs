using System;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public Guid ApplicationId { get; set; }
        public bool Criticality { get; set; } = false;
    }
}
