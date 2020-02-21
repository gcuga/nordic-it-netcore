using System;
using System.Collections.Generic;

namespace L14HW
{
    public sealed class MultipleLogWriter : BaseAbstractLogWriter
    {
        private static readonly MultipleLogWriter _instance = new MultipleLogWriter();
        private readonly List<ILogWriter> _logWriters = new List<ILogWriter>();
        public static MultipleLogWriter Instance => _instance;

        public List<ILogWriter> GetLogWriters() => new List<ILogWriter>(_logWriters);

        private MultipleLogWriter()
        {
            _logWriters.Add(ConsoleLogWriter.Instance);
            _logWriters.Add(FileLogWriter.Instance);
        }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            foreach (ILogWriter item in _logWriters)
            {
                if (item is null || item == this)
                {
                    continue;
                }

                switch (messageTypes)
                {
                    case MessageTypes.Info:
                        {
                            item.LogInfo(message);
                            break;
                        }
                    case MessageTypes.Warning:
                        {
                            item.LogWarning(message);
                            break;
                        }
                    case MessageTypes.Error:
                        {
                            item.LogError(message);
                            break;
                        }
                }
            }
        }
    }
}
