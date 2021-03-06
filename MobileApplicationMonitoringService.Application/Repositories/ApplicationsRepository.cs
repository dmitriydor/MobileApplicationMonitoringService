﻿using MobileApplicationMonitoringService.Application.Data;
using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class ApplicationsRepository : IApplicationsRepository
    {
        private readonly IDbContext context;
        private readonly IClientSessionHandle session;
        public ApplicationsRepository(IClientSessionHandle session, IDbContext context)
        {
            this.context = context;
            this.session = session;
        }

        public async Task<ApplicationData> UpsertAsync(ApplicationData data)
        {
            data.Date = DateTime.UtcNow;
            var filter = Builders<ApplicationData>.Filter.Eq(f => f.Id, data.Id);
            return await context.Applications.FindOneAndReplaceAsync(session, filter, data,
                new FindOneAndReplaceOptions<ApplicationData, ApplicationData>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task DeleteAsync(Guid id)
        {
            await context.Applications.DeleteOneAsync(session, Builders<ApplicationData>.Filter.Eq(f => f.Id, id));
        }

        public async Task<List<ApplicationData>> GetAllAsync()
        {
            return await context.Applications.Find(session, _ => true).ToListAsync();
        }

        public async Task<ApplicationData> GetByIdAsync(Guid id)
        {
            var filter = Builders<ApplicationData>.Filter.Eq(f => f.Id, id);
            return await context.Applications.Find(session, filter).FirstOrDefaultAsync();
        }

        public async Task<ApplicationData> UpdateAsync(ApplicationData data)
        {
            var filter = Builders<ApplicationData>.Filter.Eq(f => f.Id, data.Id);
            var update = Builders<ApplicationData>.Update
                .Set(f => f.UserName, data.UserName)
                .Set(f => f.OperationSystem, data.OperationSystem)
                .Set(f => f.AppVersion, data.AppVersion)
                .Set(f => f.Date, DateTime.UtcNow);
            return await context.Applications.FindOneAndUpdateAsync(session, filter, update);
        }
    }
}
