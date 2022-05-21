using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpObjectExtendingModule))]
[DependsOn(typeof(AbpIdentityApplicationContractsModule))]
[DependsOn(typeof(IdsDomainSharedModule))]
public class IdsApplicationContractsModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IdsDtoExtensions.Configure();
    }

    #endregion

}
