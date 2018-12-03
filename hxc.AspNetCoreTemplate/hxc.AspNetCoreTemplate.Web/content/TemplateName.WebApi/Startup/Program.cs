﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IWM.Core.Logging;

namespace TemplateName.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var fs = new StreamWriter(new FileStream(".PID", FileMode.CreateNew, FileAccess.Write, FileShare.Read, 64, FileOptions.DeleteOnClose)))
            {
                fs.Write(System.Diagnostics.Process.GetCurrentProcess().Id);
                fs.Flush();
                CreateWebHostBuilder(args).Build().Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddFile(opt => opt.LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
                })
                .UseKestrel()
                .UseStartup<Startup>();
        }
    }
}
