using System;
using System.Globalization;
using System.Text;

namespace L13HW
{
    public abstract class AbstractLogWriter : ILogWriter
    {
        public CultureInfo LogCultureInfo { get; }

        protected AbstractLogWriter(CultureInfo logCultureInfo)
        {
            LogCultureInfo = logCultureInfo ?? throw new ArgumentNullException(nameof(logCultureInfo));
        }

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

        protected string GetFullMessage(MessageTypes messageTypes, string message)
        {
            StringBuilder result = new StringBuilder();
            result.Append( messageTypes.GetBinaryLiteral());
            result.Append($"\u0009{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}");
            result.Append($"\u0009{messageTypes.GetName(LogCultureInfo)}");
            result.Append($"\u0009{message}");
            return result.ToString();
        }

        protected abstract void WriteLogMessage(MessageTypes messageTypes, string message);
    }
}
