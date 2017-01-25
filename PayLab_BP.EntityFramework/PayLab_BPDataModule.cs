using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using PayLab_BP.EntityFramework;

namespace PayLab_BP
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(PayLab_BPCoreModule))]
    public class PayLab_BPDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<PayLab_BPDbContext>());

            Configuration.DefaultNameOrConnectionString = "PayLab_BP";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
