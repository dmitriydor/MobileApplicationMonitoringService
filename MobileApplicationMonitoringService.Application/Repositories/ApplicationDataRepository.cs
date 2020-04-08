using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class ApplicationDataRepository : IApplicationDataRepository
    {
        private readonly IDbContext context;
        public ApplicationDataRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task<ApplicationData> UpsertAsync(ApplicationData data)
        {
            data.Date = DateTime.UtcNow;
            var filter = Builders<ApplicationData>.Filter.Eq("Id", data.Id);
            return await context.Applications.FindOneAndReplaceAsync<ApplicationData>(filter, data,
                new FindOneAndReplaceOptions<ApplicationData, ApplicationData> {
                    IsUpsert = true, 
                    ReturnDocument = ReturnDocument.After 
                });
        }

        public async Task DeleteAsync(Guid id)
        {
            await context.Applications.DeleteOneAsync(Builders<ApplicationData>.Filter.Eq("Id", id));
        }

        public async Task<IEnumerable<ApplicationData>> GetAllAsync()
        {
            return await context.Applications.Find(_ => true).ToListAsync();
        }

        public async Task<ApplicationData> GetByIdAsync(Guid id)
        {
            var filter = Builders<ApplicationData>.Filter.Eq("Id", id);
            return await context.Applications.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ApplicationData> UpdateAsync(ApplicationData data)
        {
            var filter = Builders<ApplicationData>.Filter.Eq("Id", data.Id);
            var update = Builders<ApplicationData>.Update
                .Set(f => f.UserName, data.UserName)
                .Set(f => f.OperationSystem, data.OperationSystem)
                .Set(f => f.AppVersion, data.AppVersion)
                .Set(f => f.Date, DateTime.UtcNow);
            return await context.Applications.FindOneAndUpdateAsync(filter,update);
        }
    }
}
