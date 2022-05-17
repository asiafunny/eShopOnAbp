using EShop.Localization.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShop.Localization;

[DependsOn(typeof(AbpValidationModule))]
public class EShopSharedLocalizationModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureVirtualFileSystem();
        ConfigureLocalization();
    }

    #endregion

    private void ConfigureVirtualFileSystem()
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
                                               {
                                                   options.FileSets.AddEmbedded<EShopSharedLocalizationModule>();
                                               });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Resources.Add<EShopResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/EShop");
                                              options.DefaultResourceType = typeof(EShopResource);
                                          });
    }
}
