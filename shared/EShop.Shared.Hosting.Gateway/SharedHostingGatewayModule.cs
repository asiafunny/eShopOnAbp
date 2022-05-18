using EShop.Shared.Hosting.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EShop.Shared.Hosting.Gateway;

[DependsOn(typeof(SharedHostingAspNetCoreModule))]
public class SharedHostingGatewayModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = context.Services.GetConfiguration();
        ConfigureReverseProxy(services, configuration);
    }

    #endregion

    /// <summary>
    ///     Add reverse proxy.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    private void ConfigureReverseProxy(IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy().LoadFromConfig(configuration.GetSection("ReverseProxy"));
    }
}
