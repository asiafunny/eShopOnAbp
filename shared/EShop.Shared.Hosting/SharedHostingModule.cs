using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EShop.Shared.Hosting;

/// <summary>
///     The base module for hosting.
/// </summary>
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpDataModule))]
public class SharedHostingModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureDbConnections();
    }

    #endregion

    /// <summary>
    ///     Setting the connection strings and configuring the database structures.
    /// </summary>
    /// <see>
    ///     <cref>https://docs.abp.io/en/abp/latest/Connection-Strings#abpdbconnectionoptions</cref>
    /// </see>
    private void ConfigureDbConnections()
    {
        Configure<AbpDbConnectionOptions>(options =>
                                          {
                                              options.Databases.Configure("AdministrationService",
                                                                          database =>
                                                                          {
                                                                              database.MappedConnections.Add("AbpFeatureManagement");
                                                                              database.MappedConnections.Add("AbpPermissionManagement");
                                                                              database.MappedConnections.Add("AbpSettingManagement");
                                                                              database.MappedConnections.Add("AbpAuditLogging");
                                                                              database.MappedConnections.Add("AbpBlobStoring");
                                                                          });
                                              options.Databases.Configure("IdentityService",
                                                                          database =>
                                                                          {
                                                                              database.MappedConnections.Add("AbpIdentity");
                                                                              database.MappedConnections.Add("AbpIdentityServer");
                                                                          });
                                              options.Databases.Configure("SaasService",
                                                                          database =>
                                                                          {
                                                                              database.MappedConnections.Add("AbpTenantManagement");
                                                                          });
                                          });
    }
}
