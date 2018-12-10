using System;
using Castle.Core.Logging;

namespace TemplateName.Core.Logging
{
    public class FileLoggerFactory : AbstractLoggerFactory
    {
        public FileLoggerFactory()
        {

        }

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new Log4NetLogger(LogManager.GetLogger(_loggerRepository.Name, name), this);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}
