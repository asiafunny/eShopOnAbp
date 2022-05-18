using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpSettingManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
[DependsOn(typeof(BlobStoringDatabaseEntityFrameworkCoreModule))]
[DependsOn(typeof(AdministrationServiceDomainModule))]
public class AdministrationServiceEntityFrameworkCoreModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        ConfigureDbContext(services);
    }

    #endregion

    private void ConfigureDbContext(IServiceCollection services)
    {
        services.AddAbpDbContext<AdministrationServiceDbContext>(options =>
                                                                 {
                                                                     options.ReplaceDbContext<IPermissionManagementDbContext>();
                                                                     options.ReplaceDbContext<ISettingManagementDbContext>();
                                                                     options.ReplaceDbContext<IAuditLoggingDbContext>();
                                                                     options.ReplaceDbContext<IBlobStoringDbContext>();
                                                                     options.AddDefaultRepositories(true);
                                                                 });
        Configure<AbpDbContextOptions>(options =>
                                       {
                                           options.Configure<AdministrationServiceDbContext>(config =>
                                                                                             {
                                                                                                 config.UseSqlServer(sqlBuilder =>
                                                                                                                     {
                                                                                                                         sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                                                         sqlBuilder.MigrationsHistoryTable("__AdministrationService_Migrations");
                                                                                                                     });
                                                                                             });
                                       });
    }

}
