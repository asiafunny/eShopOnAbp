using Volo.Abp.Settings;

namespace EShop.AdministrationService.Settings;

public class AdministrationSettingDefinitionProvider : SettingDefinitionProvider
{

    #region Overrides of SettingDefinitionProvider

    /// <summary>
    ///     Define your own settings here.
    /// </summary>
    /// <param name="context"></param>
    /// <example>
    ///     Example:
    ///     context.Add(new SettingDefinition(AdministrationSettings.MySetting1));
    /// </example>
    public override void Define(ISettingDefinitionContext context)
    {
    }

    #endregion

}
