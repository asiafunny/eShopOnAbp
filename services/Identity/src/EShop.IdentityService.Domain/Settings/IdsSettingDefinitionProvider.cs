using Volo.Abp.Settings;

namespace EShop.IdentityService.Settings;

public class IdsSettingDefinitionProvider : SettingDefinitionProvider
{

    #region Overrides of SettingDefinitionProvider

    /// <summary>
    ///     Define your own settings here.
    /// </summary>
    /// <param name="context"></param>
    /// <example>
    ///     Example:
    ///     context.Add(new SettingDefinition(IdentitySettings.MySetting1));
    /// </example>
    public override void Define(ISettingDefinitionContext context)
    {
    }

    #endregion

}
