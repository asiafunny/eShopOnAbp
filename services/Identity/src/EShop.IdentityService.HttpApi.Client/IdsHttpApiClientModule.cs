using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(IdsApplicationContractsModule))]
public class IdsHttpApiClientModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        ConfigureHttpClientProxies(services);
    }

    #endregion

    private void ConfigureHttpClientProxies(IServiceCollection services)
    {
        services.AddHttpClientProxies(typeof(IdsApplicationContractsModule).Assembly, IdsRemoteServiceConsts.RemoteServiceName);
    }

}
