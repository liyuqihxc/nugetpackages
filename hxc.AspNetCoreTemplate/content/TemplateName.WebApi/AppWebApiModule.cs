using System;
using Abp.AutoMapper;
using Abp.Modules;
using TemplateName.Data;
using TemplateName.WebApi.Exceptions;

namespace TemplateName.WebApi
{
    [DependsOn(typeof(AppDataModule), typeof(AbpAutoMapperModule))]
    public class AppWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper()
                .Configurators.Add(ExceptionMapperConfig.ConfigExceptionMapper);
        }
        
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);
        }
    }
}
