using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpIdentityHttpApiModule))]
[DependsOn(typeof(IdsApplicationContractsModule))]
public class IdsHttpApiModule : AbpModule
{
}
