using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.IdentityService.EntityFrameworkCore;

/// <summary>
///     This class is needed for EF Core console commands (like Add-Migration and Update-Database commands)
/// </summary>
public class IdsDbContextFactory : IDesignTimeDbContextFactory<IdsDbContext>
{

    #region Implementation of IDesignTimeDbContextFactory<out IdsDbContext>

    /// <inheritdoc />
    public IdsDbContext CreateDbContext(string[] args)
    {
        IdsEntityExtensionMappings.Configure();
        var builder = new DbContextOptionsBuilder<IdsDbContext>().UseSqlServer(GetConnectionStringFromConfiguration(),
                                                                               sqlBuilder =>
                                                                               {
                                                                                   sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                   sqlBuilder.MigrationsHistoryTable("__Ids_Migrations");
                                                                               });
        return new IdsDbContext(builder.Options);
    }

    #endregion

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(IdsDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}EShop.IdentityService.DbMigrator")).AddJsonFile("appsettings.json", false);
        return builder.Build();
    }
}
