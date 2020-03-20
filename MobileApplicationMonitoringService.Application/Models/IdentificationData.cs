using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class IdentificationData
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string OS { get; set; }
        public string AppVersion { get; set; }
        public override string ToString()
        {
            return $"Id:{Id}, Username:{UserName}, Operation System:{OS}, Application Version:{AppVersion}";
        }
    }
}
