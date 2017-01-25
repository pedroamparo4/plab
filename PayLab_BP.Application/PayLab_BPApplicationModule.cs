using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace PayLab_BP
{
    [DependsOn(typeof(PayLab_BPCoreModule), typeof(AbpAutoMapperModule))]
    public class PayLab_BPApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
