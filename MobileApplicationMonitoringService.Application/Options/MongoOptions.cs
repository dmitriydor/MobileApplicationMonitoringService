using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Options
{
    public class MongoOptions
    {
        public string Database { get; set; }
        public string ConnectionString { get; set; }
        public MongoClient MongoClient { get; set; }
    }
}
