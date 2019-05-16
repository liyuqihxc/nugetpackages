using Abp.Modules;
using System;

namespace TemplateName
{
    [DependsOn(typeof(Abp.AbpKernelModule))]
    public class AppModule : AbpModule
    {
        public AppModule()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
        }
    }
}
