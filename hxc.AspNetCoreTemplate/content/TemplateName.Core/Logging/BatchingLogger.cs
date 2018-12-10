using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Logging;

namespace TemplateName.Core.Logging
{
    public class BatchingLogger : ILogger
    {
        private readonly BatchingLoggerProvider _provider;
        public string Category { get; }

        public BatchingLogger (BatchingLoggerProvider loggerProvider, string categoryName)
        {
            _provider = loggerProvider;
            Category = categoryName;
        }

        public IDisposable BeginScope<TState> (TState state)
        {
            return null;
        }

        public bool IsEnabled (LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        // Write a log message
        public void Log<TState> (DateTimeOffset timestamp, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled (logLevel))
            {
                return;
            }

            var builder = new StringBuilder ();
            builder.Append (timestamp.ToString ("yyyy-MM-dd HH:mm:ss.fff zzz"));
            builder.Append (" [");
            builder.Append (logLevel.ToString ());
            builder.Append ("] ");
            builder.Append (Category);
            builder.Append (": ");
            builder.AppendLine (formatter (state, exception));

            if (exception != null)
            {
                builder.AppendLine (exception.ToString ());
            }

            _provider.AddMessage (timestamp, builder.ToString ());
        }

        public void Log<TState> (LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log (DateTimeOffset.Now, logLevel, eventId, state, exception, formatter);
        }
    }
}
