using EShop.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace EShop.AdministrationService;

public abstract class AdministrationServiceAppService : ApplicationService
{
    protected AdministrationServiceAppService()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
    }
}
