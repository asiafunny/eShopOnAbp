using System;
using System.Threading.Tasks;
using EShop.Shared.Hosting.AspNetCore.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EShop.IdentityService;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var applicationName = typeof(Program).Assembly.GetName().Name;
        SerilogHelper.Configure(applicationName!);
        try
        {
            Log.Information($"Starting {applicationName}...");
            var builder = Host.CreateDefaultBuilder(args)
                              .AddAppSettingsSecretsJson()
                              .ConfigureLogging((_, logging) => logging.ClearProviders())
                              .ConfigureServices((_, services) =>
                                                 {
                                                     services.AddHostedService<IdsDbMigratorHostedService>();
                                                 });
            await builder.RunConsoleAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{applicationName} terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
