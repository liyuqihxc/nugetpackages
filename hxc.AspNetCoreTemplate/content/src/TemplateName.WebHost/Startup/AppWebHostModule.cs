using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Modules;
using TemplateName.Data;
using TemplateName.WebApi;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TemplateName.WebHost
{
    [DependsOn(typeof(AppDataModule), typeof(AppWebApiModule), typeof(AbpAspNetCoreModule), typeof(AbpAutoMapperModule))]
    public class AppWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            
        }
        
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(GetType().Assembly);

            IocManager.Resolve<IAbpAspNetCoreConfiguration>().RouteConfiguration.Add(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
