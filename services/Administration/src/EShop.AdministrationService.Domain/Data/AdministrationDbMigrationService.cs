using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace EShop.AdministrationService.Data;

public class AdministrationDbMigrationService : ITransientDependency
{
    protected IEnumerable<IAdministrationDbSchemaMigrator> DbSchemaMigrators { get; }

    protected IDataSeeder DataSeeder { get; }

    protected ILogger<AdministrationDbMigrationService> Logger { get; }

    public AdministrationDbMigrationService(IEnumerable<IAdministrationDbSchemaMigrator> dbSchemaMigrators, IDataSeeder dataSeeder, ILogger<AdministrationDbMigrationService> logger)
    {
        DbSchemaMigrators = dbSchemaMigrators;
        DataSeeder = dataSeeder;
        Logger = logger;
    }

    public virtual async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Started database migrations...");
        await MigrateDatabaseSchemaAsync(cancellationToken);
        await SeedDataAsync();
        Logger.LogInformation("Successfully completed host database migrations.");
        Logger.LogInformation("You can safely end this process...");
    }

    protected async Task MigrateDatabaseSchemaAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Migrating schema for host database...");
        foreach (var migrator in DbSchemaMigrators)
        {
            await migrator.MigrateAsync(cancellationToken);
        }
    }

    protected async Task SeedDataAsync()
    {
        Logger.LogInformation("Executing host database seed...");
        await DataSeeder.SeedAsync(new DataSeedContext());
    }
}
