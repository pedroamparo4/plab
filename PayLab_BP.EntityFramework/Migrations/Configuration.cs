using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using PayLab_BP.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace PayLab_BP.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<PayLab_BP.EntityFramework.PayLab_BPDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PayLab_BP";
        }

        protected override void Seed(PayLab_BP.EntityFramework.PayLab_BPDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
