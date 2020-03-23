using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public sealed class IdentificationData
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string OperationSystem { get; set; }
        public string AppVersion { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}, Username:{UserName}, Operation System:{OperationSystem}, Application Version:{AppVersion}";
        }
    }
}
