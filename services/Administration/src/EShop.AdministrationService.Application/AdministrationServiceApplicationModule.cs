using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
[DependsOn(typeof(AdministrationServiceDomainModule))]
[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
public class AdministrationServiceApplicationModule : AbpModule
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
        services.AddAutoMapperObjectMapper<AdministrationServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
                                        {
                                            options.AddMaps<AdministrationServiceApplicationModule>(true);
                                        });
    }

}
