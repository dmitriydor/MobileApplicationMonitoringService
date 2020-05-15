using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Reflection;

namespace MobileApplicationMonitoringService.Application.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IClientSessionHandle session;

        public UnitOfWork(IServiceProvider serviceProvider, IOptions<MongoOptions> mongoOptions)
        {
            this.serviceProvider = serviceProvider;
            session = mongoOptions.Value.MongoClient.StartSession();
            session.StartTransaction();
        }
        private static ConstructorInfo GetConstructor<TImplementation>()
        {
            var ctor = typeof(TImplementation)
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();
            if (ctor == null)
            {
                throw new InvalidOperationException($"Service implementation must have at least one public constructor. None was found in class {typeof(TImplementation)}");
            }
            return ctor;
        }

        public T GetRepository<T>()
        {
            var ctor = GetConstructor<T>();
            var ctorParameters = ctor.GetParameters();
            Object[] param = ctorParameters.Select(x =>
                    (x.ParameterType == typeof(IClientSessionHandle)) ? session : serviceProvider.GetService(x.ParameterType)).ToArray();
            return (T)ctor.Invoke(param);
        }
        public void Commit()
        {
            session.CommitTransactionAsync();
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}