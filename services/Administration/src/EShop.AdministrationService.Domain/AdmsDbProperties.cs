namespace EShop.AdministrationService;

public static class AdmsDbProperties
{

    public const string ConnectionStringName = "AdministrationService";

    public static string DbTablePrefix { get; set; } = "Adms";

    public static string? DbSchema { get; set; } = null;
}
