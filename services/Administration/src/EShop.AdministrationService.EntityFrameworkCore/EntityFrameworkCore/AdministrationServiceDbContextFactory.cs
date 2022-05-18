using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.AdministrationService.EntityFrameworkCore;

public class AdministrationServiceDbContextFactory : IDesignTimeDbContextFactory<AdministrationServiceDbContext>
{

    #region Implementation of IDesignTimeDbContextFactory<out AdministrationServiceDbContext>

    /// <inheritdoc />
    public AdministrationServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AdministrationServiceDbContext>().UseSqlServer(GetConnectionStringFromConfiguration(),
                                                                                                 sqlBuilder =>
                                                                                                 {
                                                                                                     sqlBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                                                                                                     sqlBuilder.MigrationsHistoryTable("__AdministrationService_Migrations");
                                                                                                 });
        return new AdministrationServiceDbContext(builder.Options);
    }

    #endregion

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(AdministrationServiceDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}EShop.AdministrationService.HttpApi.Host")).AddJsonFile("appsettings.json", false);
        return builder.Build();
    }
}
