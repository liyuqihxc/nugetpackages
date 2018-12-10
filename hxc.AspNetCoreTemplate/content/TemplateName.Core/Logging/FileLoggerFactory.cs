using System;

namespace TemplateName.Core.Logging
{
    public class FileLoggerFactory : Castle.Core.Logging.AbstractLoggerFactory
    {
        public FileLoggerFactory()
        {
            
        }

        public override Castle.Core.Logging.ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new CastleBatchingLogger(_msLoggerFactory.CreateLogger(name), _msLoggerFactory);
        }

        public override Castle.Core.Logging.ILogger Create(string name, Castle.Core.Logging.LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}
