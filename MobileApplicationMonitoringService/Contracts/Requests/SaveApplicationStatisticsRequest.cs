using System;
using System.Collections.Generic;

namespace MobileApplicationMonitoringService.Contracts.Requests
{
    public class SaveApplicationStatisticsRequest
    {
        public class Event
        {
            public DateTime Date { get; set; }
            public string EventName { get; set; }
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public List<Event> Events { get; set; }

    }
}