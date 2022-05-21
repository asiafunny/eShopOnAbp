using System;
using System.Threading.Tasks;
using EShop.Shared.Hosting.AspNetCore.Helpers;
using Microsoft.AspNetCore.Builder;
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
            var app = await ApplicationBuilderHelper.BuildApplicationAsync<IdsHttpApiHostModule>(args);
            await app.InitializeApplicationAsync();
            await app.RunAsync();
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
