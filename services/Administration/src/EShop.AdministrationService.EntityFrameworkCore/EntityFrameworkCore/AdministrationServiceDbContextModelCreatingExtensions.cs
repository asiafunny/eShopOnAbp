using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EShop.AdministrationService.EntityFrameworkCore;

public static class AdministrationServiceDbContextModelCreatingExtensions
{
    public static void ConfigureAdministrationService(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
