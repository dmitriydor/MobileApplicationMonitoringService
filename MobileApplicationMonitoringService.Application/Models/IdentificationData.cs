using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public sealed class IdentificationData
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public List<Event> Events { get; set; }
    }
}
