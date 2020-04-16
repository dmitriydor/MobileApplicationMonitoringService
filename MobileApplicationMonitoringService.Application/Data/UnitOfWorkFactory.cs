using System;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWorkFactory:IUnitOfWorkFactory
    {
        public static IServiceProvider ServiceProvider;
        public UnitOfWorkFactory()
        {
        }

        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(ServiceProvider);
        }
    }
}