using System;
using Abp.EntityFrameworkCore;
using Abp.Modules;
using TemplateName.Core;

namespace TemplateName.Data
{
    [DependsOn(typeof(AppCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class AppDataModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(this.GetType().Assembly);
        }
    }
}
