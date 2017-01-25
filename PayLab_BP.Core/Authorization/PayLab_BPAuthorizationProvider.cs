using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace PayLab_BP.Authorization
{
    public class PayLab_BPAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
          var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var logs = context.GetPermissionOrNull(PermissionNames.Logs);
            if (logs == null)
            {
                logs = context.CreatePermission(PermissionNames.Logs, L("Logs"));
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PayLab_BPConsts.LocalizationSourceName);
        }
    }
}
