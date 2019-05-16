using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AutoMapper;
using Abp.Modules;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using TemplateName.Core;
using TemplateName.Core.Configuration;
using TemplateName.Data;
using TemplateName.WebApi;

namespace TemplateName.WebHost
{
    [DependsOn(typeof(AppWebApiModule), typeof(AbpAspNetCoreModule), typeof(AbpAutoMapperModule))]
    public class AppWebHostModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AppWebHostModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AppConsts.ConnectionStringName);
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
