using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(IdsApplicationContractsModule))]
public class IdsHttpApiClientModule : AbpModule
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
        services.AddHttpClientProxies(typeof(IdsApplicationContractsModule).Assembly, IdsRemoteServiceConsts.RemoteServiceName);
    }

}
