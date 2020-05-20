using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly IDbContext context;
        private readonly IClientSessionHandle session;
        public EventsRepository(IClientSessionHandle session, IDbContext context)
        {
            this.context = context;
            this.session = session;
        }

        public async Task CreateAsync(Event data)
        {
            await context.Events.InsertOneAsync(session, data);
        }

        public async Task CreateBatchAsync(IEnumerable<Event> events)
        {
            await context.Events.InsertManyAsync(session, events);
        }

        public async Task DeleteAllForAsync(Guid applicationId)
        {
            var filter = Builders<Event>.Filter.Eq(f => f.ApplicationId, applicationId);
            var result = await context.Events.DeleteManyAsync(session, filter);
        }

        public async Task<List<EventWithDescription>> GetAllForAsync(Guid applicationId)
        {
            var list = context.Events.AsQueryable().Where(p => p.ApplicationId == applicationId)
                .GroupJoin(
                context.EventDescriptions.AsQueryable(),
                e => e.EventName,
                ed => ed.EventName,
                (e, ed) => new EventWithDescription
                {
                    Id = e.Id,
                    ApplicationId = e.ApplicationId,
                    Date = e.Date,
                    EventName = e.EventName,
                    Description = ed.First().Description
                }
                );
            return await list.ToListAsync();
        }
    }
}
