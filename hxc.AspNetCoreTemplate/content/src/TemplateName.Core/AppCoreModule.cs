using System;
using Abp;
using Abp.Modules;
using Microsoft.Extensions.Configuration;

namespace TemplateName.Core
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AppCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
        }
    }
}
