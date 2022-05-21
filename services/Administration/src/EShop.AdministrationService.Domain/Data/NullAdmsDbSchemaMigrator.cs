using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EShop.AdministrationService.Data;

public class NullAdmsDbSchemaMigrator : IAdmsDbSchemaMigrator, ITransientDependency
{

    #region Implementation of IAdmsDbSchemaMigrator

    /// <inheritdoc />
    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    #endregion

}
