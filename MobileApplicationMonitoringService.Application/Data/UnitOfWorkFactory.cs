using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using System;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory
    {
        public static IServiceProvider ServiceProvider;
        public static MongoClientSingleton MongoClient;

        public static UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(ServiceProvider, MongoClient);
        }
    }
}