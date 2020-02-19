using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace L14CW
{
    class LogFileWriterExtended :IDisposable
    {
        private readonly StreamWriter _streamWriter;
        public string FileName { get; }

        public LogFileWriterExtended(string fileName)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            _streamWriter = new StreamWriter(
                File.Open(FileName,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.None));
        }

        public void WriteLog(string message)
        {
            _streamWriter.WriteLine($"{DateTimeOffset.UtcNow:O}\t{message}");
            _streamWriter.Flush();
            
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}
