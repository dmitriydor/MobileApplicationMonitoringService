using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class EventDescriptionsRepository : IEventDescriptionsRepository
    {
        private readonly IClientSessionHandle session;
        private readonly IDbContext context;
        public EventDescriptionsRepository(IClientSessionHandle session, IDbContext context)
        {
            this.session = session;
            this.context = context;
        }
        public async Task AddBatchEventAsync(IEnumerable<EventDescription> eventDescriptions)
        {
            var listEventDescription = (await context.EventDescriptions.Find(_ => true).ToListAsync()).Select(x => x.EventName);
            var list = eventDescriptions.Where(x => !listEventDescription.Contains(x.EventName));
            await context.EventDescriptions.InsertManyAsync(list);
        }
        public async Task UpdateBatchEventAsync(IEnumerable<EventDescription> eventDescriptions)
        {
            IEnumerable<ReplaceOneModel<EventDescription>> bulkOperations = eventDescriptions.Select(x => new ReplaceOneModel<EventDescription>(
            Builders<EventDescription>.Filter.Eq(e => e.EventName, x.EventName), x)
            {
                IsUpsert = true
            });
            await context.EventDescriptions.BulkWriteAsync(bulkOperations);
        }

        public async Task<List<EventDescription>> GetAllEvensAsync()
        {
            return await context.EventDescriptions.Find(session, _ => true).ToListAsync();
        }
        public async Task<EventDescription> GetByEventNameAsync(string eventName)
        {
            var fileter = Builders<EventDescription>.Filter.Eq(f => f.EventName, eventName);
            return await context.EventDescriptions.Find(session, fileter).FirstOrDefaultAsync();
        }
    }
}
