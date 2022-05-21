using EShop.IdentityService.EntityFrameworkCore;
using EShop.Shared.Hosting.AspNetCore;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(SharedHostingAspNetCoreModule))]
[DependsOn(typeof(IdsApplicationContractsModule))]
[DependsOn(typeof(IdsEntityFrameworkCoreModule))]
public class IdsDbMigratorModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }

    #endregion

}
