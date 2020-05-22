using System;

namespace MobileApplicationMonitoringService.Contracts.Responses
{
    public class EventWithDescriptionResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public Guid ApplicationId { get; set; }
        public bool Criticality { get; set; }
        public string Description { get; set; }
    }
}
