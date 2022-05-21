using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EShop.IdentityService.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
[DependsOn(typeof(IdsDomainModule))]
public class IdsEntityFrameworkCoreModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IdsEntityExtensionMappings.Configure();
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
        services.AddAbpDbContext<IdsDbContext>(options =>
                                               {
                                                   options.ReplaceDbContext<IIdentityDbContext>();
                                                   options.ReplaceDbContext<IIdentityServerDbContext>();
                                                   // Remove "includeAllEntities: true" to create default repositories only for aggregate roots
                                                   options.AddDefaultRepositories(true);
                                               });
        Configure<AbpDbContextOptions>(options =>
                                       {
                                           options.Configure<IdsDbContext>(config =>
                                                                           {
                                                                               config.UseSqlServer(sqlBuilder =>
                                                                                                   {
                                                                                                       sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                                       sqlBuilder.MigrationsHistoryTable("__Ids_Migrations");
                                                                                                   });
                                                                           });
                                       });
    }

}
