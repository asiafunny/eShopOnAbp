using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EShop.Shared.Hosting.Microservice.DbMigrations;

public abstract class PendingEfCoreMigrationsChecker<TDbContext> : PendingMigrationsCheckerBase
    where TDbContext : DbContext
{
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    protected IServiceProvider ServiceProvider { get; }

    protected ICurrentTenant CurrentTenant { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IAbpDistributedLock DistributedLock { get; }

    protected string DatabaseName { get; }

    protected PendingEfCoreMigrationsChecker(IUnitOfWorkManager unitOfWorkManager, IServiceProvider serviceProvider, ICurrentTenant currentTenant, IDistributedEventBus distributedEventBus,
                                             IAbpDistributedLock distributedLock, string databaseName)
    {
        UnitOfWorkManager = unitOfWorkManager;
        ServiceProvider = serviceProvider;
        CurrentTenant = currentTenant;
        DistributedEventBus = distributedEventBus;
        DistributedLock = distributedLock;
        DatabaseName = databaseName;
    }

    public virtual async Task CheckAndApplyDatabaseMigrationsAsync()
    {
        await TryAsync(LockAndApplyDatabaseMigrationsAsync);
    }

    protected virtual async Task LockAndApplyDatabaseMigrationsAsync()
    {
        await using (var handle = await DistributedLock.TryAcquireAsync($"Migration_{DatabaseName}"))
        {
            Log.Information($"Lock is acquired for db migration and data seeding on database named: {DatabaseName}...");
            if (handle == null)
            {
                Log.Information($"Handle is null because of the locking for : {DatabaseName}");
                return;
            }

            using (CurrentTenant.Change(null))
            {
                using (var uow = UnitOfWorkManager.Begin(true))
                {
                    var dbContext = ServiceProvider.GetRequiredService<TDbContext>();
                    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                    if (pendingMigrations.Any())
                    {
                        await dbContext.Database.MigrateAsync();
                    }

                    await uow.CompleteAsync();
                }

                var dataSeeder = ServiceProvider.GetRequiredService<IDataSeeder>();
                await dataSeeder.SeedAsync();
            }

            Log.Information($"Lock is released for db migration and data seeding on database named: {DatabaseName}...");
        }
    }
}
