using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;
using System.Dynamic;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMongoDatabase database;
        public UnitOfWorkFactory(IServiceProvider serviceProvider, IMongoDatabase database)
        {
            this.serviceProvider = serviceProvider;
            this.database = database;
        }
        
        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(serviceProvider, database.Client);
        }
    }
}