using Abp.MultiTenancy;
using PayLab_BP.Users;

namespace PayLab_BP.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public virtual int? TenantType { get; set; }

        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}