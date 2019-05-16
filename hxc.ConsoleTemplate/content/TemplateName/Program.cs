using Abp;
using Castle.Windsor.MsDependencyInjection;
using hxc.Logging.RollingFileLogger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace TemplateName
{
    class Program
    {
        static int Main(string[] args)
        {
            // Create service collection
            var serviceCollection = new ServiceCollection();

            // Run app
            return ConfigureServices(serviceCollection).GetService<MainApp>().Run(args);
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add logging
            services.AddLogging();
            services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
            services.Configure((FileLoggerOptions opt) =>
            {
                opt.LogDirectory = "Logs";
            });

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            services.AddSingleton(configuration);

            // Add app
            services.AddTransient<MainApp>();

            var abpBootstrapper = AbpBootstrapper.Create<AppModule>();
            services.AddSingleton(abpBootstrapper);
            var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(abpBootstrapper.IocManager.IocContainer, services);
            abpBootstrapper.Initialize();
            return serviceProvider;
        }
    }
}
