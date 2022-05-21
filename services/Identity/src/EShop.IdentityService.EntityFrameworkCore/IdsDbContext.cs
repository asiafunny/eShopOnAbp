using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;

namespace EShop.IdentityService.EntityFrameworkCore;

[ConnectionStringName(IdsDbProperties.ConnectionStringName)]
public class IdsDbContext : AbpDbContext<IdsDbContext>, IIdentityDbContext, IIdentityServerDbContext
{
    /// <inheritdoc />
    public IdsDbContext(DbContextOptions<IdsDbContext> options)
        : base(options)
    {
    }

    #region Implementation of IIdentityDbContext

    /// <inheritdoc />
    public DbSet<IdentityUser> Users { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityRole> Roles { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityClaimType> ClaimTypes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityLinkUser> LinkUsers { get; set; } = default!;

    #endregion

    #region Implementation of IIdentityServerDbContext

    /// <inheritdoc />
    public DbSet<ApiResource> ApiResources { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiResourceSecret> ApiResourceSecrets { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiResourceScope> ApiResourceScopes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiScope> ApiScopes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityResource> IdentityResources { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityResourceClaim> IdentityClaims { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<Client> Clients { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientGrantType> ClientGrantTypes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientScope> ClientScopes { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientSecret> ClientSecrets { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientClaim> ClientClaims { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<ClientProperty> ClientProperties { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<PersistedGrant> PersistedGrants { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; } = default!;

    #endregion

    #region Overrides of AbpDbContext<IdsDbContext>

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureIds();
    }

    #endregion

}
