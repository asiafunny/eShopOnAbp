using Volo.Abp.Threading;

namespace EShop.IdentityService;

public static class IdsGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    /// <summary>
    ///     Configure (enable/disable) global features of the used modules here.
    /// </summary>
    /// <see>
    ///     Please refer to the documentation to lear more about the Global Features System:
    ///     <cref>https://docs.abp.io/en/abp/latest/Global-Features</cref>
    /// </see>
    public static void Configure()
    {
        OneTimeRunner.Run(() =>
                            {
                            });
    }
}
