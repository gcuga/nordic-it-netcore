using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace L13HW
{
    class FileLogWriter : AbstractLogWriter
    {
        public string FilePath { get; }
        public string FileName { get; }

        public string FullPath => FilePath + FileName;

        public FileLogWriter() : this(GetDefaultPath(), "mydefault.log", CultureInfo.CurrentCulture) { }

        public FileLogWriter(string path, string fileName, CultureInfo logCultureInfo) : base(logCultureInfo)
        {
            FilePath = path ?? throw new ArgumentNullException(nameof(path));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetDefaultPath()
        {
            string defaultPath = Assembly.GetExecutingAssembly().Location;
            if (defaultPath is null || defaultPath == string.Empty)
            {
                throw new DirectoryNotFoundException("Папка сборки не найдена");
            }

            defaultPath = defaultPath.Remove(defaultPath.LastIndexOf('\\') + 1) + @"MyLog\";
            if (Directory.Exists(defaultPath) == false)
            {
                Directory.CreateDirectory(defaultPath);
            }

            return defaultPath;
        }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            using (var fs = File.OpenWrite(FullPath))
            {
                fs.Seek(0, SeekOrigin.End);
                using (TextWriter tw = new StreamWriter(fs))
                {
                    tw.WriteLine(GetFullMessage(messageTypes, message));
                }
            }
        }
    }
}
