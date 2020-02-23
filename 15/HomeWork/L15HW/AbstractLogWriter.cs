using System;
using System.Globalization;
using System.Text;

namespace L15HW
{
    public abstract class AbstractLogWriter : BaseAbstractLogWriter
    {
        public CultureInfo LogCultureInfo { get; }

        protected AbstractLogWriter(CultureInfo logCultureInfo)
        {
            LogCultureInfo = logCultureInfo ?? throw new ArgumentNullException(nameof(logCultureInfo));
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
     }
}
