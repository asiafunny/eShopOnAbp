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

    /// <summary>
    ///     Adding the supported languages.
    /// </summary>
    /// <see>
    ///     <cref>https://docs.abp.io/en/abp/latest/Localization</cref>
    /// </see>
    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Languages.Add(new LanguageInfo("en", "en", "English"));
                                              options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                                              options.Resources.Add<EShopResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/Resources");
                                              options.DefaultResourceType = typeof(EShopResource);
                                          });
    }
}
