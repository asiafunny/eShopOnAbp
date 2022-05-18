using EShop.Localization.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShop.Localization;

[DependsOn(typeof(AbpValidationModule))]
public class SharedLocalizationModule : AbpModule
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
                                                   options.FileSets.AddEmbedded<SharedLocalizationModule>();
                                               });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Resources.Add<SharedResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/EShop");
                                              options.DefaultResourceType = typeof(SharedResource);
                                          });
    }
}
