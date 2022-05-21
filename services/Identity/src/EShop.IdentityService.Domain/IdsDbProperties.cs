namespace EShop.IdentityService;

public static class IdsDbProperties
{

    public const string ConnectionStringName = "IdentityService";

    public static string DbTablePrefix { get; set; } = "Ids";

    public static string? DbSchema { get; set; } = null;
}
