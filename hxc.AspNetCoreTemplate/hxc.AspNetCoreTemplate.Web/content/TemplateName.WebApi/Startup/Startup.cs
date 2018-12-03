using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IWM.Core.StratumProxy.Extensions;
using IWM.Core.Configuration;

namespace TemplateName.WebApi
{
    // https://blog.csdn.net/sD7O95O/article/details/81117353
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddStratumProxy(config =>
            {
                config.Server = new Uri(Configuration["StratumProxy:Pool"]);
                config.Password = Configuration["StratumProxy:Password"];
                config.UseSsl = bool.Parse(Configuration["StratumProxy:UseSsl"]);
                config.WalletAddress = Configuration["StratumProxy:WalletAddress"];
                config.UserName = Configuration["StratumProxy:UserName"];
                config.MaxMinersPerConnection = int.Parse(Configuration["StratumProxy:MaxMinersPerConnection"]);
            });

            services.AddSignalR();

            services.AddMvc((options) =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services.AddAbp<IWMWebApiModule>(options =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAbp();

            app.UseStratumProxy();

            SignalR.IWMSignalRModule.Config(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<Exceptions.ErrorHandlingMiddleware>();

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                app.ApplicationServices.GetRequiredService<IAbpAspNetCoreConfiguration>()
                    .RouteConfiguration
                    .ConfigureAll(routes);
            });
        }
    }
}
