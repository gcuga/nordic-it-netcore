using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace L15HW
{
    class Program
    {
        static void Main(string[] args)
        {
            LogWriterFactory factory = LogWriterFactory.Instance;

            ILogWriter clw = factory.GetLogWriter<ConsoleLogWriter>();
            ILogWriter flw = factory.GetLogWriter<FileLogWriter>();

            List<ILogWriter> logWriterList = new List<ILogWriter>();
            logWriterList.Add(clw);
            logWriterList.Add(flw);
            // конструктор с параметром
            ILogWriter mlw = factory.GetLogWriter<MultipleLogWriter>(logWriterList);

            ILogWriter ilw1 = factory.GetLogWriter<ConsoleLogWriter>();
            if (ilw1 == clw)
            {
                ilw1.LogInfo("Console message");
            }

            ILogWriter ilw2 = factory.GetLogWriter<FileLogWriter>();
            if (ilw2 == flw)
            {
                ilw2.LogWarning("File message");
            }

            // при возврате имеющегося инстанса параметр не нужен
            // таким образом в случае параметризованного конструктора
            // получаем антипаттерн для синглтона, если для объекта требуется
            // конструктор с параметрами, то этот объект не годится для
            // применения к нему паттерна синглтон, если при задании новых 
            // параметров проверять, что они новые и пересоздавать объект,
            // то это уже вообще будет не синглтон, переменные которым был
            // присвоен инстанс ранее и новые переменные будут иметь ссылку
            // на разные инстансы, т.е. объектов станет много
            ILogWriter ilw3 = factory.GetLogWriter<MultipleLogWriter>();
            if (ilw3 == mlw)
            {
                ilw3.LogError("Multiple message");
            }
        }
    }
}
