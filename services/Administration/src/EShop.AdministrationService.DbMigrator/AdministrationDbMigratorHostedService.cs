using System.Threading;
using System.Threading.Tasks;
using EShop.AdministrationService.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace EShop.AdministrationService;

public class AdministrationDbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IConfiguration _configuration;

    public AdministrationDbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
        _configuration = configuration;
    }

    #region Implementation of IHostedService

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var application = await AbpApplicationFactory.CreateAsync<AdministrationDbMigratorModule>(options =>
                                                                                                         {
                                                                                                             options.Services.ReplaceConfiguration(_configuration);
                                                                                                             options.UseAutofac();
                                                                                                             options.Services.AddLogging(builder => builder.AddSerilog());
                                                                                                         }))
        {
            await application.InitializeAsync();
            await application.ServiceProvider.GetRequiredService<AdmsDbMigrationService>().MigrateAsync(cancellationToken);
            await application.ShutdownAsync();
            _hostApplicationLifetime.StopApplication();
        }
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    #endregion

}
