using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.Modularity;

namespace EShop.Shared.Hosting.AspNetCore.Helpers;

public static class ApplicationBuilderHelper
{
    /// <summary>
    ///     Build a web application.
    /// </summary>
    /// <typeparam name="TStartupModule"></typeparam>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task<WebApplication> BuildApplicationAsync<TStartupModule>(string[] args)
        where TStartupModule : IAbpModule
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.AddAppSettingsSecretsJson().UseAutofac().UseSerilog();
        await builder.AddApplicationAsync<TStartupModule>();
        return builder.Build();
    }
}
