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

        public async Task<IdentificationData> Create(IdentificationData data)
        {
            data.Date = DateTime.UtcNow;
            data.Id = Guid.NewGuid();
            await context.IdentificationList.InsertOneAsync(data);
            return await GetById(data.Id);
        }

        public async Task Delete(Guid id)
        {
            await context.IdentificationList.DeleteOneAsync(Builders<IdentificationData>.Filter.Eq("Id", id));
        }

        public async Task<IEnumerable<IdentificationData>> GetAll()
        {
            return await context.IdentificationList.Find(_ => true).ToListAsync();
        }

        public async Task<IdentificationData> GetById(Guid id)
        {
            var filter = Builders<IdentificationData>.Filter.Eq("Id", id);
            return await context.IdentificationList.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IdentificationData> Update(IdentificationData data)
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
