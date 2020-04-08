using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbContext context;
        public EventRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Event data)
        {
            await context.Events.InsertOneAsync(data);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Event>> GetAllForAsync(Guid identificationId)
        {
            var filter = Builders<Event>.Filter.Eq("IdentificationId", identificationId);
            return await context.Events.Find(filter).ToListAsync();

        }

        public Task UpdateAsync(Event data)
        {
            throw new NotImplementedException();
        }
    }
}
