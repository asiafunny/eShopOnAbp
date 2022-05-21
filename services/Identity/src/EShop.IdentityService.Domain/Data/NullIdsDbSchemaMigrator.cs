using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EShop.IdentityService.Data;

public class NullIdsDbSchemaMigrator : IIdsDbSchemaMigrator, ITransientDependency
{

    #region Implementation of IIdentityServiceDbSchemaMigrator

    /// <inheritdoc />
    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    #endregion

}
