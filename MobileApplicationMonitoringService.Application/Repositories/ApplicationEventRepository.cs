﻿using MobileApplicationMonitoringService.Application.Data;
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
        private readonly IClientSessionHandle session;
        public ApplicationEventRepository(IClientSessionHandle session, DbContext context)
        {
            this.context = context;
            this.session = session;
        }

        public async Task CreateAsync(ApplicationEvent data)
        {
            await context.Events.InsertOneAsync(session, data);
        }

        public async Task CreateBatchAsync(IEnumerable<ApplicationEvent> events)
        {
            await context.Events.InsertManyAsync(session,events);
        }

        public async Task DeleteAllForAsync(Guid applicationId)
        {
            var filter = Builders<ApplicationEvent>.Filter.Eq("ApplicationId", applicationId);
            await context.Events.DeleteManyAsync(session,filter);
        }

        public async Task<List<ApplicationEvent>> GetAllForAsync(Guid applicationId)
        {
            var filter = Builders<ApplicationEvent>.Filter.Eq("ApplicationId", applicationId);
            var list = await context.Events.Find(session,filter).ToListAsync();
            return list;
        }

        public Task UpdateAsync(ApplicationEvent data)
        {
            //TODO: Implement method UpdateAsync
            throw new NotImplementedException();
        }
    }
}
