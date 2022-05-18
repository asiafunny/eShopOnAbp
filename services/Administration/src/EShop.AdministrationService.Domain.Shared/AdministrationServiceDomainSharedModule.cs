using EShop.AdministrationService.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
[DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
[DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
[DependsOn(typeof(BlobStoringDatabaseDomainSharedModule))]
public class AdministrationServiceDomainSharedModule : AbpModule
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
                                                   options.FileSets.AddEmbedded<AdministrationServiceDomainSharedModule>();
                                               });
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
                                          {
                                              options.Resources.Add<AdministrationServiceResource>("en").AddBaseTypes(typeof(AbpValidationResource)).AddVirtualJson("/Localization/AdministrationService");
                                              options.DefaultResourceType = typeof(AdministrationServiceResource);
                                          });
    }
}
