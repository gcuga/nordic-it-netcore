using System;
using System.Globalization;

namespace L14HW
{
    public sealed class ConsoleLogWriter : AbstractLogWriter
    {
        private static readonly ConsoleLogWriter _instance = new ConsoleLogWriter();

        public static ConsoleLogWriter Instance
        {
            get
            {
                return _instance;
            }
        }

        private ConsoleLogWriter() { }

        protected override void WriteLogMessage(MessageTypes messageTypes, string message)
        {
            Console.WriteLine(GetFullMessage(messageTypes, message));
        }
    }
}
