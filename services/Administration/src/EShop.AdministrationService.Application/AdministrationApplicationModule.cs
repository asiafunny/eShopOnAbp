using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AdministrationDomainModule))]
[DependsOn(typeof(AdministrationApplicationContractsModule))]
public class AdministrationApplicationModule : AbpModule
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
        services.AddAutoMapperObjectMapper<AdministrationApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
                                        {
                                            options.AddMaps<AdministrationApplicationModule>(true);
                                        });
    }

}
