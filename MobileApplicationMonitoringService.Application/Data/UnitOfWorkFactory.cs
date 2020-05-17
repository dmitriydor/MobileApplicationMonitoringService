using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using System;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory
    {
        public static IServiceProvider ServiceProvider;
        public static IOptions<MongoOptions> Options;

        public static UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(ServiceProvider, Options);
        }
    }
}