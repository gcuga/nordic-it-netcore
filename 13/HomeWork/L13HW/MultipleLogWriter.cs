using System;
using System.Collections.Generic;

namespace L13HW
{
    class MultipleLogWriter : ILogWriter
    {
        public List <ILogWriter> LogWriters { get; set; }

        public MultipleLogWriter(List<ILogWriter> logWriters)
        {
            LogWriters = logWriters ?? throw new ArgumentNullException(nameof(logWriters));
        }

        public void LogInfo(string message)
        {
            ForeachCallLogOperation(MessageTypes.Info, message);
        }

        public void LogWarning(string message)
        {
            ForeachCallLogOperation(MessageTypes.Warning, message);
        }

        public void LogError(string message)
        {
            ForeachCallLogOperation(MessageTypes.Error, message);
        }

        private void ForeachCallLogOperation(MessageTypes messageType, string message) 
        {
            foreach (ILogWriter item in LogWriters)
            {
                if (item is null || item == this)
                {
                    continue;
                }

                switch (messageType)
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
