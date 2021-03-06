using EShop.AdministrationService.EntityFrameworkCore;
using EShop.Shared.Hosting.AspNetCore;
using Volo.Abp.Modularity;

namespace EShop.AdministrationService;

[DependsOn(typeof(SharedHostingAspNetCoreModule))]
[DependsOn(typeof(AdmsApplicationContractsModule))]
[DependsOn(typeof(AdmsEntityFrameworkCoreModule))]
public class AdministrationDbMigratorModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }

    #endregion

}
