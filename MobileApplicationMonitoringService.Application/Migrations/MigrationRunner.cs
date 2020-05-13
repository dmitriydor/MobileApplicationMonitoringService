using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileApplicationMonitoringService.Application.Migrations
{
    public class MigrationRunner
    {
        private readonly IMongoDatabase db;
        private readonly MigrationLoader migrationLoader;
        public MigrationRunner(IOptions<MongoOptions> options)
        {
            db = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.Database);
            this.migrationLoader = new MigrationLoader();
        }
        private void ApplyMigrations(IEnumerable<IMigration> migrations)
        {
            foreach (IMigration migration in migrations)
            {
                try
                {
                    migration.Up(db);
                }
                catch (Exception e)
                {

                    throw new Exception($"Migration failed to by applied: {e.Message} \n {migration.Version}, {migration.Description}, Name: {migration.GetType()}, Database: {db.ToString()}");
                }
                GetDbMigrations().InsertOne(new MigrationModel { Version = migration.Version, Description = migration.Description });
            }
        }

        private IMongoCollection<MigrationModel> GetDbMigrations()
        {
            return db.GetCollection<MigrationModel>("Migrations");
        }

        private MigrationModel GetLatestDbMigration()
        {
            var listDbMigrations = GetDbMigrations().Find(_ => true).ToList();
            if (listDbMigrations.Count == 0)
            {
                return new MigrationModel();
            }

            return listDbMigrations
                .OrderByDescending(m => m.Version)
                .FirstOrDefault();
        }

        public void Update(Version version)
        {
            if (migrationLoader.LatestVersion() == GetLatestDbMigration()?.Version)
            {
                return;
            }

            var currentMigration = GetLatestDbMigration();
            var unappliedMigrations = migrationLoader.GetUnappliedMigrations(currentMigration).Where(m => m.Version <= version);
            ApplyMigrations(unappliedMigrations);
        }

        public void UpdateToLatestMigration()
        {
            Update(migrationLoader.LatestVersion());
        }

    }
}