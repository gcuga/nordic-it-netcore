using System;
using System.Collections.Generic;

namespace L15HW
{
    class MultipleLogWriter : BaseAbstractLogWriter
    {
        public List <ILogWriter> LogWriters { get; set; }

        public MultipleLogWriter(List<ILogWriter> logWriters)
        {
            LogWriters = logWriters ?? throw new ArgumentNullException(nameof(logWriters));
        }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            foreach (ILogWriter item in LogWriters)
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
