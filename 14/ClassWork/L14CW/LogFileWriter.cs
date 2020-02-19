using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace L14CW
{
    class LogFileWriter
    {
        public string FileName { get; }

        public LogFileWriter(string fieName)
        {
            FileName = fieName ?? throw new ArgumentNullException(nameof(fieName));
        }

        public void WriteLog(string message)
        {
            var sw = new StreamWriter(
                File.Open(FileName,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.None));

            sw.WriteLine($"{DateTimeOffset.UtcNow:O}\t{message}");
            //sw.Flush();
            sw.Close();
        }
    }
}
