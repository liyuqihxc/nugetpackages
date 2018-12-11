using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hxc.Logging.RollingFileLogger
{
    [ProviderAlias("File")]
    public class FileLoggerProvider : BatchingLoggerProvider
    {
        private readonly string _path;
        private readonly string _fileName;
        private readonly int? _maxFileSize;
        private readonly int? _maxRetainedFiles;

        public FileLoggerProvider(IOptions<FileLoggerOptions> options) : base(options)
        {
            var loggerOptions = options.Value;
            _path = loggerOptions.LogDirectory;
            _fileName = loggerOptions.FileName;
            _maxFileSize = loggerOptions.FileSizeLimit;
            _maxRetainedFiles = loggerOptions.RetainedFileCountLimit;
        }

        // Write the provided messages to the file system
        protected override async Task WriteMessagesAsync(IEnumerable<LogMessage> messages, CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(_path);

            // Group messages by log date
            foreach (var group in messages.GroupBy(GetGrouping))
            {
                var fullName = GetFullName(group.Key);

                // Write the log messages to the file
                using (var streamWriter = File.AppendText(fullName))
                {
                    foreach (var item in group)
                    {
                        await streamWriter.WriteAsync(item.Message);
                    }
                }
            }

            RollFiles();
        }

        // Get the file name
        private string GetFullName((int Year, int Month, int Day) group)
        {
            int index = 1;
            while (true)
            {
                var fullName = Path.Combine(_path, $"{_fileName}{group.Year:0000}{group.Month:00}{group.Day:00}-{index}.txt");
                var fileInfo = new FileInfo(fullName);

                if (_maxFileSize > 0 && fileInfo.Exists && fileInfo.Length >= _maxFileSize)
                    index++;
                else
                    return fullName;
            }
        }

        private (int Year, int Month, int Day) GetGrouping(LogMessage message)
        {
            return (message.Timestamp.Year, message.Timestamp.Month, message.Timestamp.Day);
        }

        // Delete files if we have too many
        protected void RollFiles()
        {
            if (_maxRetainedFiles > 0)
            {
                var files = new DirectoryInfo(_path)
                    .GetFiles(_fileName + "*")
                    .OrderByDescending(f => f.Name)
                    .Skip(_maxRetainedFiles.Value);

                foreach (var item in files)
                {
                    item.Delete();
                }
            }
        }
    }
}