using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationMonitoringService.Application.Options
{
    public class MongoOptions:IMongoOptions
    {
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        
        public MongoOptions(IConfiguration configuration)
        {
            Database = configuration["MongoOptions:Database"];
            ConnectionString = configuration["MongoOptions:ConnectionString"];
        }
    }
}
