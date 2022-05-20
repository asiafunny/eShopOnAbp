using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace EShop.Shared.Hosting.AspNetCore;

[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(SharedHostingModule))]
public class SharedHostingAspNetCoreModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }

    #endregion

}
