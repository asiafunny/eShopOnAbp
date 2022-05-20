using System.Threading;
using System.Threading.Tasks;

namespace EShop.AdministrationService.Data;

public interface IAdmsDbSchemaMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
