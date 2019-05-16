using System;
using Abp.AutoMapper;
using Abp.Modules;
using TemplateName.Data;

namespace TemplateName.App
{
    [DependsOn(typeof(AppDataModule), typeof(AbpAutoMapperModule))]
    public class AppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
        }
    }
}
