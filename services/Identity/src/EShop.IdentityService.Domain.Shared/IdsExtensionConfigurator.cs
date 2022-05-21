using Volo.Abp.Threading;

namespace EShop.IdentityService;

public static class IdsExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
                            {
                                ConfigureExistingProperties();
                                ConfigureExtraProperties();
                            });
    }

    /// <summary>
    ///     You can change max lengths for properties of the entities defined in the modules used by your application.
    ///     Notice: It is not suggested to change property lengths unless you really need it. Go with the standard values
    ///     wherever possible.
    ///     If you are using EF Core, you will need to run the add-migration command after your changes.
    /// </summary>
    /// <example>
    ///     Example: Change user and role name max lengths:
    ///     IdentityUserConsts.MaxNameLength = 99;
    ///     IdentityRoleConsts.MaxNameLength = 99;
    /// </example>
    private static void ConfigureExistingProperties()
    {
    }

    /// <summary>
    ///     You can configure extra properties for the entities defined in the modules used by your application.
    ///     This class can be used to define these extra properties with a high level, easy to use API.
    /// </summary>
    /// <example>
    ///     Example: Add a new property to the user entity of the identity module
    ///     ObjectExtensionManager.Instance.Modules()
    ///         .ConfigureIdentity(identity =>
    ///             {
    ///                 identity.ConfigureUser(user =>
    ///                     {
    ///                         user.AddOrUpdateProperty<string>( //property type: string
    ///                             "SocialSecurityNumber", //property name
    ///                             property =>
    ///                                 {
    ///                                     //validation rules
    ///                                     property.Attributes.Add(new RequiredAttribute());
    ///                                     property.Attributes.Add(new StringLengthAttribute(64) { MinimumLength = 4 });
    ///                                     //...other configurations for this property
    ///                                 });
    ///                      });
    ///              });
    /// </example>
    /// <see>
    ///     <cref>https://docs.abp.io/en/abp/latest/Module-Entity-Extensions</cref>
    /// </see>
    private static void ConfigureExtraProperties()
    {
    }

}
