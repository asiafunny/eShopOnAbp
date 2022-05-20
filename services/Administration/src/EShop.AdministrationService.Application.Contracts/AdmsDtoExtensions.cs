using Volo.Abp.Threading;

namespace EShop.AdministrationService;

public static class AdmsDtoExtensions
{
    private static readonly OneTimeRunner s_OneTimeRunner = new();

    /// <summary>
    ///     Add extension properties to DTOs defined in the depended modules.
    /// </summary>
    /// <example>
    ///     Example:
    ///     ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityRoleDto, string>("Title");
    /// </example>
    /// <see>
    ///     See the documentation for more:
    ///     <cref>https://docs.abp.io/en/abp/latest/Object-Extensions</cref>
    /// </see>
    public static void Configure()
    {
        s_OneTimeRunner.Run(() =>
                            {
                            });
    }
}
