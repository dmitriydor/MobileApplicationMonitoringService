using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class ApplicationEventRepository : IApplicationEventRepository
    {
        private readonly IDbContext context;
        public ApplicationEventRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(ApplicationEvent data)
        {
            await context.Events.InsertOneAsync(data);
        }

        public async Task CreateBatchAsync(IEnumerable<ApplicationEvent> events)
        {
            await context.Events.InsertManyAsync(events);
        }

        public async Task DeleteAllForAsync(Guid applicationId)
        {
            var filter = Builders<ApplicationEvent>.Filter.Eq("ApplicationId", applicationId);
            await context.Events.DeleteManyAsync(filter);
        }

        public async Task<List<ApplicationEvent>> GetAllForAsync(Guid applicationId)
        {
            var filter = Builders<ApplicationEvent>.Filter.Eq("ApplicationId", applicationId);
            var list = await context.Events.Find(filter).ToListAsync();
            return list;
        }

        public Task UpdateAsync(ApplicationEvent data)
        {
            //TODO: Implement method UpdateAsync
            throw new NotImplementedException();
        }
    }
}
