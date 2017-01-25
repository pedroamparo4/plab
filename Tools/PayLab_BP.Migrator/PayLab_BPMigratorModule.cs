using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using PayLab_BP.EntityFramework;

namespace PayLab_BP.Migrator
{
    [DependsOn(typeof(PayLab_BPDataModule))]
    public class PayLab_BPMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<PayLab_BPDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}