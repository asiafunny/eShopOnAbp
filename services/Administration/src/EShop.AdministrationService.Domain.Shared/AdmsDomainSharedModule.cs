using EShop.AdministrationService.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpFeatureManagementDomainSharedModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
[DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
[DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
[DependsOn(typeof(BlobStoringDatabaseDomainSharedModule))]
public class AdmsDomainSharedModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdmsGlobalFeatureConfigurator.Configure();
        AdmsExtensionConfigurator.Configure();
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
                                                   options.FileSets.AddEmbedded<AdmsDomainSharedModule>();
                                               });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Resources.Add<AdmsResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/Administration");
                                              options.DefaultResourceType = typeof(AdmsResource);
                                          });
    }

    private void ConfigureExceptionLocalization()
    {
        Configure<AbpExceptionLocalizationOptions>(options =>
                                                   {
                                                       options.MapCodeNamespace("EShop.Administration", typeof(AdmsResource));
                                                   });
    }

}
