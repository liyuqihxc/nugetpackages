using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateName.Core.Logging
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }

        public string Message { get; set; }
    }
}
