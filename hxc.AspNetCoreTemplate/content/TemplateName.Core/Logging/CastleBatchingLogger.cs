using System;

using Microsoft.Extensions.Logging;

namespace TemplateName.Core.Logging
{
    public class CastleBatchingLogger : Castle.Core.Logging.ILogger
    {
        private readonly BatchingLogger _batchingLogger;
        private readonly Microsoft.Extensions.Logging.ILoggerFactory _msLoggerFactory;

        public CastleBatchingLogger(Microsoft.Extensions.Logging.ILogger msLogger, Microsoft.Extensions.Logging.ILoggerFactory msLoggerFactory)
        {
            _batchingLogger = msLogger as BatchingLogger;
            _msLoggerFactory = msLoggerFactory;
        }

        public bool IsDebugEnabled => _batchingLogger.IsEnabled(LogLevel.Debug);

        public bool IsErrorEnabled => _batchingLogger.IsEnabled(LogLevel.Error);

        public bool IsFatalEnabled => _batchingLogger.IsEnabled(LogLevel.Critical);

        public bool IsInfoEnabled => _batchingLogger.IsEnabled(LogLevel.Information);

        public bool IsWarnEnabled => _batchingLogger.IsEnabled(LogLevel.Warning);

        public Castle.Core.Logging.ILogger CreateChildLogger(string loggerName)
        {
            var msLogger = _msLoggerFactory.CreateLogger(_batchingLogger.Category + "." + loggerName);
            return new CastleBatchingLogger(msLogger, _msLoggerFactory);
        }

        public void Debug(string message)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(message);
        }

        public void Debug(Func<string> messageFactory)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(messageFactory());
        }

        public void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(exception, message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(format, args);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(exception, format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(string.Format(formatProvider, format, args));
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsDebugEnabled)
                _batchingLogger.LogDebug(exception, string.Format(formatProvider, format, args));
        }

        public void Error(string message)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(message);
        }

        public void Error(Func<string> messageFactory)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(messageFactory());
        }

        public void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(exception, message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(format, args);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(exception, format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(string.Format(formatProvider, format, args));
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsErrorEnabled)
                _batchingLogger.LogError(exception, string.Format(formatProvider, format, args));
        }

        public void Fatal(string message)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(message);
        }

        public void Fatal(Func<string> messageFactory)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(messageFactory());
        }

        public void Fatal(string message, Exception exception)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(exception, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(format, args);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(exception, format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(string.Format(formatProvider, format, args));
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsFatalEnabled)
                _batchingLogger.LogCritical(exception, string.Format(formatProvider, format, args));
        }

        public void Info(string message)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(message);
        }

        public void Info(Func<string> messageFactory)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(messageFactory());
        }

        public void Info(string message, Exception exception)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(exception, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(format, args);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(exception, format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(string.Format(formatProvider, format, args));
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsInfoEnabled)
                _batchingLogger.LogInformation(exception, string.Format(formatProvider, format, args));
        }

        public void Warn(string message)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(message);
        }

        public void Warn(Func<string> messageFactory)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(messageFactory());
        }

        public void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(message, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(format, args);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(exception, format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(string.Format(formatProvider, format, args));
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            if (IsWarnEnabled)
                _batchingLogger.LogWarning(exception, string.Format(formatProvider, format, args));
        }
    }
}
