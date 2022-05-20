using Volo.Abp.Threading;

namespace EShop.AdministrationService;

public static class AdministrationGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner s_OneTimeRunner = new();

    /// <summary>
    ///     Configure (enable/disable) global features of the used modules here.
    /// </summary>
    /// <see>
    ///     Please refer to the documentation to lear more about the Global Features System:
    ///     <cref>https://docs.abp.io/en/abp/latest/Global-Features</cref>
    /// </see>
    public static void Configure()
    {
        s_OneTimeRunner.Run(() =>
                            {
                            });
    }
}
