using Volo.Abp.Threading;

namespace EShop.AdministrationService;

public static class AdmsEntityExtensionMappings
{
    private static readonly OneTimeRunner s_OneTimeRunner = new();

    /// <summary>
    ///     Configure extra properties for the entities defined in the modules used by your application.
    ///     This class can be used to map these extra properties to table fields in the database.
    /// </summary>
    /// <remarks>
    ///     USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
    ///     USE ModuleExtensionConfigurator CLASS (in the Domain.Shared project) FOR A HIGH LEVEL API TO DEFINE EXTRA
    ///     PROPERTIES TO ENTITIES OF THE USED MODULES
    /// </remarks>
    /// <example>
    ///     Example: Map a property to a table field:
    ///     ObjectExtensionManager.Instance.MapEfCoreProperty
    ///     <IdentityUser, string>("MyProperty", (entityBuilder, propertyBuilder) => propertyBuilder.HasMaxLength(128));
    /// </example>
    /// <see>
    ///     See the documentation for more:
    ///     <cref>https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities</cref>
    /// </see>
    public static void Configure()
    {
        AdmsGlobalFeatureConfigurator.Configure();
        AdmsExtensionConfigurator.Configure();
        s_OneTimeRunner.Run(() =>
                            {
                            });
    }
}
