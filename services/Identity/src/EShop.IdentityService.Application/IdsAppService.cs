using EShop.IdentityService.Localization;
using Volo.Abp.Application.Services;

namespace EShop.IdentityService;

public abstract class IdsAppService : ApplicationService
{
    protected IdsAppService()
    {
        LocalizationResource = typeof(IdsResource);
    }
}
