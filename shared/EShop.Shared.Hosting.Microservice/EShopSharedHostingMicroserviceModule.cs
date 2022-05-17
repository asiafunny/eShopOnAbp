using EShop.Shared.Hosting.AspNetCore;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace EShop.Shared.Hosting.Microservice;

[DependsOn(typeof(AbpAspNetCoreMultiTenancyModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpDistributedLockingModule))]
[DependsOn(typeof(AbpEventBusRabbitMqModule))]
[DependsOn(typeof(AbpBackgroundJobsRabbitMqModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
[DependsOn(typeof(EShopSharedHostingAspNetCoreModule))]
public class EShopSharedHostingMicroserviceModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = context.Services.GetConfiguration();
        ConfigureMultiTenancy();
        ConfigureDistributedCache();
        ConfigureDataProtection(services, configuration);
        ConfigureDistributedLock(services, configuration);
    }

    #endregion

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
                                          {
                                              options.IsEnabled = true;
                                          });
    }

    private void ConfigureDistributedCache()
    {
        Configure<AbpDistributedCacheOptions>(options =>
                                              {
                                                  options.KeyPrefix = "eShop:";
                                              });
    }

    private void ConfigureDataProtection(IServiceCollection services, IConfiguration configuration)
    {
        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
        services.AddDataProtection().SetApplicationName("AdministrationService").PersistKeysToStackExchangeRedis(redis, "EShop-Protection-Keys");
    }

    private void ConfigureDistributedLock(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDistributedLockProvider>(_ =>
                                                        {
                                                            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                                                            return new RedisDistributedSynchronizationProvider(redis.GetDatabase());
                                                        });
    }
}
