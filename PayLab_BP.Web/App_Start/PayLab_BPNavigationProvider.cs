using Abp.Application.Navigation;
using Abp.Localization;
using PayLab_BP.Authorization;

namespace PayLab_BP.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class PayLab_BPNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", PayLab_BPConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "#tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                               ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "#users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Logs",
                        L("Logs"),
                        url: "#logs",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Logs
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Lots",
                        new LocalizableString("Lots", PayLab_BPConsts.LocalizationSourceName),
                        url: "#/lots",
                        icon: "fa fa-info"
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PayLab_BPConsts.LocalizationSourceName);
        }
    }
}
