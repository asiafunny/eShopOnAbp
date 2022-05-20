using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
[DependsOn(typeof(AdministrationApplicationContractsModule))]
public class AdministrationHttpApiClientModule : AbpModule
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
        services.AddHttpClientProxies(typeof(AdministrationApplicationContractsModule).Assembly, AdministrationRemoteServiceConsts.RemoteServiceName);
    }

}
