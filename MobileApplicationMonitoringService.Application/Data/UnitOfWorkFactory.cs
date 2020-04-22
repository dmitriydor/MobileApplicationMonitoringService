using System;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory
    {
        public static IServiceProvider ServiceProvider;

        public static UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(ServiceProvider);
        }
    }
}