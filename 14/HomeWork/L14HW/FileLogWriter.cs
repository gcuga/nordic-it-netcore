using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using L14HW.Properties;

namespace L14HW
{
    public sealed class FileLogWriter : AbstractLogWriter
    {
        private static volatile FileLogWriter _instance;
        private static object syncRoot = new Object();
        public string FilePath { get; }

        public static FileLogWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new FileLogWriter();
                    }
                }

                return _instance;
            }
        }

        private FileLogWriter()
        {
            FilePath = Resources.ResourceManager.GetString("LogFilePath", LogCultureInfo);
            string directory = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(FilePath));

            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(FilePath) == false)
            {
                using (File.CreateText(FilePath)) { }
            }
        }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            File.AppendAllText(FilePath, $"{ GetFullMessage(messageTypes, message)}\n");
        }
    }
}
