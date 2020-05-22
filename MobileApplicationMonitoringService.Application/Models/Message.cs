using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class Message
    {
        public string UserName { get; set; }
        public string AppVersion { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime Date { get; set; }
    }
}
