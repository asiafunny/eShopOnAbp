using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpObjectExtendingModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationContractsModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
[DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
[DependsOn(typeof(AdministrationDomainSharedModule))]
public class AdministrationApplicationContractsModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdministrationDtoExtensions.Configure();
    }

    #endregion

}
