using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.AdministrationService.EntityFrameworkCore;

public class AdministrationDbContextFactory : IDesignTimeDbContextFactory<AdministrationDbContext>
{

    #region Implementation of IDesignTimeDbContextFactory<out AdministrationServiceDbContext>

    /// <inheritdoc />
    public AdministrationDbContext CreateDbContext(string[] args)
    {
        AdministrationEntityExtensionMappings.Configure();
        var builder = new DbContextOptionsBuilder<AdministrationDbContext>().UseSqlServer(GetConnectionStringFromConfiguration(),
                                                                                                 sqlBuilder =>
                                                                                                 {
                                                                                                     sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                                     sqlBuilder.MigrationsHistoryTable("__AdministrationService_Migrations");
                                                                                                 });
        return new AdministrationDbContext(builder.Options);
    }

    #endregion

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(AdministrationDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}EShop.AdministrationService.DbMigrator")).AddJsonFile("appsettings.json", false);
        return builder.Build();
    }
}
