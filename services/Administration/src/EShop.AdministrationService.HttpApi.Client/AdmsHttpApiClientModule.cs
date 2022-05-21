using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
[DependsOn(typeof(AdmsApplicationContractsModule))]
public class AdmsHttpApiClientModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureHttpClientBuilder();
    }

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        ConfigureHttpClientProxies(services);
    }

    #endregion

    private void ConfigureHttpClientBuilder()
    {
        PreConfigure<AbpHttpClientBuilderOptions>(options =>
                                                  {
                                                      options.ProxyClientBuildActions.Add((_, builder) =>
                                                                                          {
                                                                                              builder.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i))));
                                                                                          });
                                                  });
    }

    private void ConfigureHttpClientProxies(IServiceCollection services)
    {
        services.AddHttpClientProxies(typeof(AdmsApplicationContractsModule).Assembly, AdmsRemoteServiceConsts.RemoteServiceName);
    }

}
