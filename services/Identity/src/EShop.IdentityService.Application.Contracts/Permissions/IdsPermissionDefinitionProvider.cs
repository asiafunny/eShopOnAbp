using EShop.IdentityService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EShop.IdentityService.Permissions;

public class IdsPermissionDefinitionProvider : PermissionDefinitionProvider
{

    #region Overrides of PermissionDefinitionProvider

    /// <inheritdoc />
    public override void Define(IPermissionDefinitionContext context)
    {
        // var myGroup = context.AddGroup(IdentityPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(eCommercePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    #endregion

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdsResource>(name);
    }

}
