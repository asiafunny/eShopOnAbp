using EShop.AdministrationService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EShop.AdministrationService.Permissions;

public class AdmsPermissionDefinitionProvider : PermissionDefinitionProvider
{

    #region Overrides of PermissionDefinitionProvider

    /// <inheritdoc />
    public override void Define(IPermissionDefinitionContext context)
    {
        // var myGroup = context.AddGroup(AdministrationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(eCommercePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    #endregion

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AdmsResource>(name);
    }

}
