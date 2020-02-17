using System;
using System.Collections.Generic;
using System.Text;

namespace L13HW
{
    public abstract class BaseAbstractLogWriter : ILogWriter
    {
        public void LogInfo(string message)
        {
            WriteLogMessage(MessageTypes.Info, message);
        }

        public void LogWarning(string message)
        {
            WriteLogMessage(MessageTypes.Warning, message);
        }

        public void LogError(string message)
        {
            WriteLogMessage(MessageTypes.Error, message);
        }

        protected abstract void WriteLogMessage(MessageTypes messageTypes, string message);
    }
}
