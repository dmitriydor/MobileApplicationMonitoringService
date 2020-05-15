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

        public async Task UpsertEventAsync (EventDescription eventDescription)
        {
            var filter = Builders<EventDescription>.Filter.Eq("EventName", eventDescription.EventName);
            await context.EventDescriptions.FindOneAndReplaceAsync(session, filter, eventDescription, 
                new FindOneAndReplaceOptions<EventDescription>
                {
                    IsUpsert = true
                });
        }

        public async Task<List<EventDescription>> GetAllEvensAsync()
        {
            return await context.EventDescriptions.Find(session, _ => true).ToListAsync();
        }
        public async Task<EventDescription> GetByEventNameAsync(string eventName)
        {
            var fileter = Builders<EventDescription>.Filter.Eq("EventName", eventName);
            return await context.EventDescriptions.Find(session, fileter).FirstOrDefaultAsync();
        }
    }
}
