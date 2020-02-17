using System;
using System.Globalization;

namespace L13HW
{
    public class ConsoleLogWriter : AbstractLogWriter
    {
        public ConsoleLogWriter() : this(CultureInfo.CurrentCulture) { }
        public ConsoleLogWriter(CultureInfo logCultureInfo) : base(logCultureInfo) { }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            Console.WriteLine(GetFullMessage(messageTypes, message));
        }
    }
}
