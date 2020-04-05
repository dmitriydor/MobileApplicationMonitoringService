using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MobileApplicationMonitoringService.Application.Models
{
    public class MigrationReflection
    {
        private readonly List<IMigration> migrations = new List<IMigration>();

        public MigrationReflection()
        {
            LoadMigrations();
        }
        public IEnumerable<IMigration> GetAllMigrations()
        {
            return migrations;
        }

        private void LoadMigrations()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
             migrations.AddRange(assemblies
                 .SelectMany(a => a.DefinedTypes
                     .Where(t =>t.ImplementedInterfaces.Contains(typeof(IMigration)) && !t.IsAbstract  )
                     .Select(t => Activator.CreateInstance(t.AsType()))
                     .OfType<IMigration>())
                 .OrderBy(m => m.Version)
             ); 
        }

        public Version LatestVersion()
        {
            return migrations.Max(m => m.Version);
        }

        public IEnumerable<IMigration> GetUnappliedMigrations(MigrationModel currentMigration)
        {
            if (currentMigration != null)
            {
                return migrations.Where(m => m.Version > currentMigration.Version).OrderBy(m => m.Version);
            }

            return null;
        }
        
    }
}