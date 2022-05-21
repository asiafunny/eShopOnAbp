using System.Threading;
using System.Threading.Tasks;

namespace EShop.IdentityService.Data;

public interface IIdsDbSchemaMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
