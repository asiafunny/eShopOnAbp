using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdministrationDbProperties.ConnectionStringName)]
public interface IAdministrationDbContext : IEfCoreDbContext
{
}
