using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Options
{
    public interface IMongoOptions
    {
        public string Database { get; set; }
        public string ConnectionString { get; set; }
    }
}
