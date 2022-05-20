using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EShop.AdministrationService.Data;

public class NullAdministrationDbSchemaMigrator : IAdministrationDbSchemaMigrator, ITransientDependency
{

    #region Implementation of IAdministrationServiceDbSchemaMigrator

    /// <inheritdoc />
    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    #endregion

}
