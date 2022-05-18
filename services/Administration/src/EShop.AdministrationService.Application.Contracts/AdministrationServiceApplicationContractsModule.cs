using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpPermissionManagementApplicationContractsModule))]
[DependsOn(typeof(AbpSettingManagementApplicationContractsModule))]
[DependsOn(typeof(AdministrationServiceDomainSharedModule))]
public class AdministrationServiceApplicationContractsModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
    }

    #endregion

}
