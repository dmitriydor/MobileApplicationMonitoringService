using MobileApplicationMonitoringService.Application.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Repositories
{
    public class IdentificationRepository : IIdentificationRepository
    {
        private readonly IDbContext context;
        public IdentificationRepository(IDbContext context)
        {
            this.context = context;
        }

        public async Task<IdentificationData> CreateAsync(IdentificationData data)
        {
            data.Date = DateTime.UtcNow;
            data.Id = Guid.NewGuid();
            await context.IdentificationList.InsertOneAsync(data);
            return await GetByIdAsync(data.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await context.IdentificationList.DeleteOneAsync(Builders<IdentificationData>.Filter.Eq("Id", id));
        }

        public async Task<IEnumerable<IdentificationData>> GetAllAsync()
        {
            return await context.IdentificationList.Find(_ => true).ToListAsync();
        }

        public async Task<IdentificationData> GetByIdAsync(Guid id)
        {
            var filter = Builders<IdentificationData>.Filter.Eq("Id", id);
            return await context.IdentificationList.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IdentificationData> UpdateAsync(IdentificationData data)
        {
            var filter = Builders<IdentificationData>.Filter.Eq("Id", data.Id);
            var update = Builders<IdentificationData>.Update
                .Set(f => f.UserName, data.UserName)
                .Set(f => f.OperationSystem, data.OperationSystem)
                .Set(f => f.AppVersion, data.AppVersion)
                .Set(f => f.Date, DateTime.UtcNow);
            return await context.IdentificationList.FindOneAndUpdateAsync(filter,update);
        }
    }
}
