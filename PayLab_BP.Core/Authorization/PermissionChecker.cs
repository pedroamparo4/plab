using Abp.Authorization;
using PayLab_BP.Authorization.Roles;
using PayLab_BP.MultiTenancy;
using PayLab_BP.Users;

namespace PayLab_BP.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
