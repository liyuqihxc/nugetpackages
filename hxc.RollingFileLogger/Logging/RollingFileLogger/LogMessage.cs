using System;
using System.Collections.Generic;
using System.Text;

namespace hxc.Logging.RollingFileLogger
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
