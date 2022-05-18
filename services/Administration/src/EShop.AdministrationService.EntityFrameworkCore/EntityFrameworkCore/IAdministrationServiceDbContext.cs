using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public interface IAdministrationServiceDbContext : IEfCoreDbContext
{
}
