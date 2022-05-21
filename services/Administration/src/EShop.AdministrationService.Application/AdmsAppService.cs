using EShop.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace EShop.AdministrationService;

public abstract class AdmsAppService : ApplicationService
{
    protected AdmsAppService()
    {
        LocalizationResource = typeof(AdmsResource);
    }
}
