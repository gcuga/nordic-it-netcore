using System;
using System.Globalization;
using System.Text;

namespace L14HW
{
    public abstract class AbstractLogWriter : BaseAbstractLogWriter
    {
        public CultureInfo LogCultureInfo { get; } = CultureInfo.CurrentCulture;

        protected string GetFullMessage(MessageTypes messageTypes, string message)
        {
            StringBuilder result = new StringBuilder();
            result.Append( messageTypes.GetBinaryLiteral());
            result.Append($"\t{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}");
            result.Append($"\t{messageTypes.GetName(LogCultureInfo)}");
            result.Append($"\t{message}");
            return result.ToString();
        }
     }
}
