using System.Threading;
using System.Threading.Tasks;

namespace EShop.AdministrationService.Data;

public interface IAdministrationDbSchemaMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
