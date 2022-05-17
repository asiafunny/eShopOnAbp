using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EShop.Shared.Hosting.AspNetCore;

[Dependency(ReplaceServices = true)]
public class EShopBrandingProvider : DefaultBrandingProvider
{

    #region Overrides of DefaultBrandingProvider

    /// <inheritdoc />
    public override string AppName
    {
        get
        {
            return "eShop";
        }
    }

    #endregion

}
