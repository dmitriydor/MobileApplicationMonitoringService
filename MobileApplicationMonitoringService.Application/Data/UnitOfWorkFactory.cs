using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using System;
using System.Dynamic;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly MongoClientSingleton mongoClient;
        public UnitOfWorkFactory(IServiceProvider serviceProvider, MongoClientSingleton mongoClient)
        {
            this.serviceProvider = serviceProvider;
            this.mongoClient = mongoClient;
        }
        
        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(serviceProvider, mongoClient);
        }
    }
}