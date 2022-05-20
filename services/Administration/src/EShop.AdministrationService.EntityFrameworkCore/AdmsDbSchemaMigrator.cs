using System;
using System.Threading;
using System.Threading.Tasks;
using EShop.AdministrationService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EShop.AdministrationService.Data;

public class AdmsDbSchemaMigrator : IAdmsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public AdmsDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #region Implementation of IAdmsDbSchemaMigrator

    /// <summary>
    ///     We intentionally resolving the DbContext from IServiceProvider (instead of directly injecting it) to properly get
    ///     the connection string of the current tenant in the current scope.
    /// </summary>
    /// <returns></returns>
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        var dbContext = _serviceProvider.GetService<AdmsDbContext>();
        if (dbContext != null)
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    #endregion

}
