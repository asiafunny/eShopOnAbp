namespace EShop.AdministrationService;

public static class AdministrationServiceDbProperties
{

    public const string ConnectionStringName = "AdministrationService";

    public static string DbTablePrefix { get; set; } = "Ads";

    public static string? DbSchema { get; set; } = null;
}
