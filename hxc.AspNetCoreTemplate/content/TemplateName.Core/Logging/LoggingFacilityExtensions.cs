using System;
using Abp.Dependency;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Logging;

namespace TemplateName.Core.Logging
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility AddFileLogger(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<FileLoggerFactory>();
        }
    }
}
