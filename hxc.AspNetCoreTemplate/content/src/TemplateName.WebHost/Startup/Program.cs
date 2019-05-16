using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TemplateName.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("hosting.json", optional : true)
                .AddCommandLine(args)
                .Build();

            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(cfg)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional : true);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                        .AddConsole()
                        .AddDebug();
                })
                .UseKestrel()
                .UseStartup<Startup>();
        }
    }
}
