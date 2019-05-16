using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TemplateName
{
    public class MainApp
    {
        public ILogger<MainApp> Logger { get; set; }

        public MainApp()
        {
            Logger = NullLogger<MainApp>.Instance;
        }

        public int Run(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
            return 0;
        }
    }
}
