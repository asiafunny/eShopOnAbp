using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.AdministrationService.EntityFrameworkCore;

public class AdmsDbContextFactory : IDesignTimeDbContextFactory<AdmsDbContext>
{

    #region Implementation of IDesignTimeDbContextFactory<out AdmsDbContext>

    /// <inheritdoc />
    public AdmsDbContext CreateDbContext(string[] args)
    {
        AdmsEntityExtensionMappings.Configure();
        var builder = new DbContextOptionsBuilder<AdmsDbContext>().UseSqlServer(GetConnectionStringFromConfiguration(),
                                                                                sqlBuilder =>
                                                                                {
                                                                                    sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                    sqlBuilder.MigrationsHistoryTable("__Adms_Migrations");
                                                                                });
        return new AdmsDbContext(builder.Options);
    }

    #endregion

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(AdmsDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}EShop.AdministrationService.DbMigrator")).AddJsonFile("appsettings.json", false);
        return builder.Build();
    }
}
