using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(IdsDomainModule))]
[DependsOn(typeof(IdsApplicationContractsModule))]
public class IdsApplicationModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        ConfigureAutoMapper(services);
    }

    #endregion

    private void ConfigureAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapperObjectMapper<IdsApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
                                        {
                                            options.AddMaps<IdsApplicationModule>(true);
                                        });
    }

}
