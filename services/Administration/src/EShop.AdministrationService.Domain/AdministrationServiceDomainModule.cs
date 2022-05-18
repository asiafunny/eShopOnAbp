using Volo.Abp.AuditLogging;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;

namespace EShop.AdministrationService;

[DependsOn(typeof(AbpPermissionManagementDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityServerModule))]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
[DependsOn(typeof(AbpAuditLoggingDomainModule))]
[DependsOn(typeof(BlobStoringDatabaseDomainModule))]
[DependsOn(typeof(AdministrationServiceDomainSharedModule))]
public class AdministrationServiceDomainModule : AbpModule
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
                                              options.Languages.Add(new LanguageInfo("en", "en", "English", "gb"));
                                              options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文", "cn"));
                                          });
    }
}
