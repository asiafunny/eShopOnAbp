using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpIdentityServerDomainModule))]
[DependsOn(typeof(IdsDomainSharedModule))]
public class IdsDomainModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    #endregion

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Languages.Add(new LanguageInfo("en", "en", "English", "us"));
                                              options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
                                          });
    }
}
