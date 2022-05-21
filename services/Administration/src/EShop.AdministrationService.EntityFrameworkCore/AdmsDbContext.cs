using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EShop.AdministrationService.EntityFrameworkCore;

[ConnectionStringName(AdmsDbProperties.ConnectionStringName)]
public class AdmsDbContext : AbpDbContext<AdmsDbContext>, IFeatureManagementDbContext, IPermissionManagementDbContext, ISettingManagementDbContext, IAuditLoggingDbContext, IBlobStoringDbContext
{
    /// <inheritdoc />
    public AdmsDbContext(DbContextOptions<AdmsDbContext> options)
        : base(options)
    {
    }

    #region Implementation of IFeatureManagementDbContext

    /// <inheritdoc />
    public DbSet<FeatureValue> FeatureValues { get; set; } = default!;

    #endregion

    #region Implementation of IPermissionManagementDbContext

    /// <inheritdoc />
    public DbSet<PermissionGrant> PermissionGrants { get; set; } = default!;

    #endregion

    #region Implementation of ISettingManagementDbContext

    /// <inheritdoc />
    public DbSet<Setting> Settings { get; set; } = default!;

    #endregion

    #region Implementation of IAuditLoggingDbContext

    /// <inheritdoc />
    public DbSet<AuditLog> AuditLogs { get; set; } = default!;

    #endregion

    #region Implementation of IBlobStoringDbContext

    /// <inheritdoc />
    public DbSet<DatabaseBlobContainer> BlobContainers { get; set; } = default!;

    /// <inheritdoc />
    public DbSet<DatabaseBlob> Blobs { get; set; } = default!;

    #endregion

    #region Overrides of AbpDbContext<AdmsDbContext>

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureBlobStoring();
        builder.ConfigureAdms();
    }

    #endregion

}
