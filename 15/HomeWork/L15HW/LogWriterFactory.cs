using System;
using System.Collections.Generic;
using System.Text;

namespace L15HW
{
    public sealed class LogWriterFactory
    {
        private static LogWriterFactory _instance;
        private List<ILogWriter> _logWriters = new List<ILogWriter>();

        public static LogWriterFactory Instance => _instance ?? (_instance = new LogWriterFactory());

        private LogWriterFactory() { }

        public ILogWriter GetLogWriter<T>(object parameters = null) where T : ILogWriter
        {
            Type type = typeof(T);

            // Type t = T.GetType(); T недопустим в данном контексте
            // bool b = T is ConsoleLogWriter; T недопустим в данном контексте

            foreach (var item in _logWriters)
            {
                if (type == item.GetType())
                {
                    return item;
                }
            }

            ILogWriter logWriter = null;
            if (type == typeof(ConsoleLogWriter))
            {
                logWriter = new ConsoleLogWriter();
            }
            else if (type == typeof(FileLogWriter))
            {
                logWriter = new FileLogWriter();
            }
            else if (type == typeof(MultipleLogWriter))
            {
                // parameters is List<ILogWriter> - не дает ожидаемый результат при выполнении
                // typeof(parameters) parameters является переменной, но используется как тип
                if (parameters == null || parameters.GetType() != typeof(List<ILogWriter>))
                {
                    throw new Exception("Для создания экземпляра MultipleLogWriter ноебходимо задать правильные параметры");
                }
                List<ILogWriter> constructorParameter = (List<ILogWriter>)parameters;
                logWriter = new MultipleLogWriter(constructorParameter);
            }

            if (logWriter == null)
            {
                throw new Exception("Данный тип логгера не поддерживается фабрикой");
            }

            _logWriters.Add(logWriter);
            return logWriter;
        }
    }
}
