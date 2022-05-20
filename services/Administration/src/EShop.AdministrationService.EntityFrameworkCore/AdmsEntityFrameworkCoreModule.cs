using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(AbpFeatureManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpSettingManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
[DependsOn(typeof(BlobStoringDatabaseEntityFrameworkCoreModule))]
[DependsOn(typeof(AdmsDomainModule))]
public class AdmsEntityFrameworkCoreModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdmsEntityExtensionMappings.Configure();
    }

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        ConfigureDbContext(services);
    }

    #endregion

    private void ConfigureDbContext(IServiceCollection services)
    {
        services.AddAbpDbContext<AdmsDbContext>(options =>
                                                                 {
                                                                     options.ReplaceDbContext<IFeatureManagementDbContext>();
                                                                     options.ReplaceDbContext<IPermissionManagementDbContext>();
                                                                     options.ReplaceDbContext<ISettingManagementDbContext>();
                                                                     options.ReplaceDbContext<IAuditLoggingDbContext>();
                                                                     options.ReplaceDbContext<IBlobStoringDbContext>();
                                                                     // Remove "includeAllEntities: true" to create default repositories only for aggregate roots
                                                                     options.AddDefaultRepositories(true);
                                                                 });
        Configure<AbpDbContextOptions>(options =>
                                       {
                                           options.Configure<AdmsDbContext>(config =>
                                                                                             {
                                                                                                 config.UseSqlServer(sqlBuilder =>
                                                                                                                     {
                                                                                                                         sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                                                         sqlBuilder.MigrationsHistoryTable("__Adms_Migrations");
                                                                                                                     });
                                                                                             });
                                       });
    }

}
