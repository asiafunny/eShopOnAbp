using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EShop.IdentityService.EntityFrameworkCore;

public static class IdsDbContextModelCreatingExtensions
{
    public static void ConfigureIds(this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
