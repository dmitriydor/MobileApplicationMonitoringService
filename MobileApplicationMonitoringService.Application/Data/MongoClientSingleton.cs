using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class MongoClientSingleton
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
        public MongoClientSingleton(IOptions<MongoOptions> options)
        {
            Client = new MongoClient(options.Value.ConnectionString);
            Database = Client.GetDatabase(options.Value.Database);
        }
    }
}
