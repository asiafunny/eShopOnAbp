using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EShop.Shared.Hosting.Gateway.Extensions;

public static class HostBuilderExtensions
{
    public const string AppYarpJsonPath = "yarp.json";

    /// <summary>
    ///     Add Yarp reverse proxy settings json file to the host environment.
    /// </summary>
    /// <param name="hostBuilder"></param>
    /// <param name="path"></param>
    /// <param name="optional"></param>
    /// <param name="reloadOnChange"></param>
    /// <returns></returns>
    public static IHostBuilder AddYarpJson(this IHostBuilder hostBuilder, string path = AppYarpJsonPath, bool optional = true, bool reloadOnChange = true)
    {
        return hostBuilder.ConfigureAppConfiguration((_, builder) =>
                                                     {
                                                         builder.AddJsonFile(path, optional, reloadOnChange).AddEnvironmentVariables();
                                                     });
    }
}
