﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class EventWithDescription
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
