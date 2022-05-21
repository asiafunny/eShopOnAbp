using EShop.IdentityService.Localization;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShop.IdentityService;

[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpIdentityDomainSharedModule))]
[DependsOn(typeof(AbpIdentityServerDomainSharedModule))]
public class IdsDomainSharedModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IdsGlobalFeatureConfigurator.Configure();
        IdsExtensionConfigurator.Configure();
    }

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureVirtualFileSystem();
        ConfigureLocalization();
        ConfigureExceptionLocalization();
    }

    #endregion

    private void ConfigureVirtualFileSystem()
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
                                               {
                                                   options.FileSets.AddEmbedded<IdsDomainSharedModule>();
                                               });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Resources.Add<IdsResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/Identity");
                                              options.DefaultResourceType = typeof(IdsResource);
                                          });
    }

    private void ConfigureExceptionLocalization()
    {
        Configure<AbpExceptionLocalizationOptions>(options =>
                                                   {
                                                       options.MapCodeNamespace("EShop.Identity", typeof(IdsResource));
                                                   });
    }

}
