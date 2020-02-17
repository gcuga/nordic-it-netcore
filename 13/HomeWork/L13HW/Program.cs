using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace L13HW
{
    class Program
    {
        static void Main(string[] args)
        {
            // первый список, параметры по умолчанию
            List<ILogWriter> logWritersFirst = new List<ILogWriter>();
            logWritersFirst.Add(new ConsoleLogWriter());
            logWritersFirst.Add(null);
            FileLogWriter fileLogWriter = new FileLogWriter();
            logWritersFirst.Add(fileLogWriter);
            Console.WriteLine($"Default culture (current) - {fileLogWriter.LogCultureInfo}");
            Console.WriteLine($"Default path - {fileLogWriter.FullPath}");
            Console.WriteLine("===================================");
            Console.WriteLine("Первый список:");
            foreach (var item in logWritersFirst)
            {
                Console.WriteLine((item is null) ? "null" : item.GetType().Name);
            }
            Console.WriteLine("===================================");

            MultipleLogWriter multipleLogWriterFirst = new MultipleLogWriter(logWritersFirst);

            //второй список
            List<ILogWriter> logWritersSecond = new List<ILogWriter>();
            logWritersSecond.Add(multipleLogWriterFirst);
            logWritersSecond.Add(new FileLogWriter(@"C:\L13HW\Den\MyLog\", "mylogfile1.txt", CultureInfo.InvariantCulture));
            logWritersSecond.Add(new FileLogWriter(@"C:\L13HW\Den\MyLog\", "mylogfile2.txt", CultureInfo.CreateSpecificCulture("ru-RU")));

            MultipleLogWriter multipleLogWriterSecond = new MultipleLogWriter(logWritersSecond);

            // добавим вишенку на торт
            logWritersSecond.Add(null);
            logWritersSecond.Add(multipleLogWriterSecond);

            Console.WriteLine("Второй список:");
            foreach (var item in multipleLogWriterSecond.LogWriters)
            {
                Console.WriteLine((item is null) ? "null" : item.GetType().Name);
            }

            Console.WriteLine("===================================");
            Console.WriteLine("Вывод в консоль и три файла:");
            multipleLogWriterSecond.LogInfo("message 1");
            multipleLogWriterSecond.LogWarning("message 2");
            multipleLogWriterSecond.LogError("message 3");
            multipleLogWriterSecond.LogInfo("message inf 4");
            multipleLogWriterSecond.LogInfo("message inf 5");
            multipleLogWriterSecond.LogWarning("message warn 6");
            multipleLogWriterSecond.LogWarning("message warn 7");
            multipleLogWriterSecond.LogError("message err 8");
            multipleLogWriterSecond.LogError("message err 9");
        }
    }
}
