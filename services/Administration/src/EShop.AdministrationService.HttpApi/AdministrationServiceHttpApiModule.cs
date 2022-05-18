using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpPermissionManagementHttpApiModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiModule))]
[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
public class AdministrationServiceHttpApiModule : AbpModule
{

}
