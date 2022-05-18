using System;
using System.Linq;
using System.Threading.Tasks;
using EShop.AdministrationService.EntityFrameworkCore;
using EShop.Shared.Hosting.Microservice.DbMigrations;
using Serilog;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace EShop.AdministrationService.DbMigratioins;

public class AdministrationServiceDatabaseMigrationChecker : PendingEfCoreMigrationsChecker<AdministrationServiceDbContext>
{
    protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
    protected IPermissionDataSeeder PermissionDataSeeder { get; }

    /// <inheritdoc />
    public AdministrationServiceDatabaseMigrationChecker(IUnitOfWorkManager unitOfWorkManager, IServiceProvider serviceProvider, ICurrentTenant currentTenant, IDistributedEventBus distributedEventBus,
                                                         IAbpDistributedLock distributedLock, IPermissionDefinitionManager permissionDefinitionManager, IPermissionDataSeeder permissionDataSeeder)
        : base(unitOfWorkManager,
               serviceProvider,
               currentTenant,
               distributedEventBus,
               distributedLock,
               AdministrationServiceDbProperties.ConnectionStringName)
    {
        PermissionDefinitionManager = permissionDefinitionManager;
        PermissionDataSeeder = permissionDataSeeder;
    }

    #region Overrides of PendingEfCoreMigrationsChecker<AdministrationServiceDbContext>

    /// <inheritdoc />
    public override async Task CheckAndApplyDatabaseMigrationsAsync()
    {
        await base.CheckAndApplyDatabaseMigrationsAsync();
        await TryAsync(SeedDataAsync);
    }

    #endregion

    private async Task SeedDataAsync()
    {
        await using (var handle = await DistributedLock.TryAcquireAsync($"Migration_{DatabaseName}"))
        {
            Log.Information($"Lock is acquired for data seeding on database named: {DatabaseName}...");
            if (handle == null)
            {
                Log.Information($"Handle is null because of the locking for : {DatabaseName}");
                return;
            }

            using (CurrentTenant.Change(null))
            {
                using (var uow = UnitOfWorkManager.Begin(true))
                {
                    var permissions = PermissionDefinitionManager.GetPermissions().Where(p => p.MultiTenancySide.HasFlag(MultiTenancySides.Host) && (p.Providers.Count == 0 || p.Providers.Contains(RolePermissionValueProvider.ProviderName))).Select(p => p.Name).ToList();
                    await PermissionDataSeeder.SeedAsync(RolePermissionValueProvider.ProviderName, "Admin", permissions);
                    await uow.CompleteAsync();
                }
            }

            Log.Information($"Lock is released for data seeding on database named: {DatabaseName}...");
        }
    }
}
