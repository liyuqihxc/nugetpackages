using System;
using Abp;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TemplateName.Core
{
    [DependsOn(typeof(AbpKernelModule))]
    public class TemplateNameCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
        }
    }
}
