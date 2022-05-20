using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdmsDbProperties.ConnectionStringName)]
public interface IAdmsDbContext : IEfCoreDbContext
{
}
