using System;
using System.Threading;
using System.Threading.Tasks;
using EShop.AdministrationService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EShop.AdministrationService.Data;

public class AdministrationDbSchemaMigrator : IAdministrationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public AdministrationDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    #region Implementation of IAdministrationServiceDbSchemaMigrator

    /// <summary>
    ///     We intentionally resolving the DbContext from IServiceProvider (instead of directly injecting it) to properly get
    ///     the connection string of the current tenant in the current scope.
    /// </summary>
    /// <returns></returns>
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        var dbContext = _serviceProvider.GetService<AdministrationDbContext>();
        if (dbContext != null)
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }

    #endregion

}
