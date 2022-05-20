using EShop.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace EShop.AdministrationService;

public abstract class AdministrationAppService : ApplicationService
{
    protected AdministrationAppService()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}
