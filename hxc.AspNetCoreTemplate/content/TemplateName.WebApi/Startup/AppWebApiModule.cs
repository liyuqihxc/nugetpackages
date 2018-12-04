using System;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Modules;
using TemplateName.Data;
using TemplateName.WebApi.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace TemplateName.WebApi
{
    [DependsOn(typeof(AppDataModule), typeof(AbpAspNetCoreModule), typeof(AbpAutoMapperModule))]
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

            IocManager.Resolve<IAbpAspNetCoreConfiguration>().RouteConfiguration.Add(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
