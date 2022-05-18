using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
public class AdministrationServiceHttpApiClientModule : AbpModule
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
        services.AddHttpClientProxies(typeof(AdministrationServiceApplicationContractsModule).Assembly, AdministrationServiceRemoteServiceConsts.RemoteServiceName);
    }

}
