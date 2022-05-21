using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EShop.AdministrationService.EntityFrameworkCore;

public static class AdmsDbContextModelCreatingExtensions
{
    public static void ConfigureAdms(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
