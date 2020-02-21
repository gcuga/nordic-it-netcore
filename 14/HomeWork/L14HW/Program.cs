using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace L14HW
{
    class Program
    {
        static void Main(string[] args)
        {
            MultipleLogWriter m = MultipleLogWriter.Instance;
            m.LogInfo("MultipleLogWriter - info");
            m.LogWarning("MultipleLogWriter - warning");
            m.LogError("MultipleLogWriter - error");
            Console.WriteLine("===========================================");

            ConsoleLogWriter.Instance.LogInfo("ConsoleLogWriter - info");
            ConsoleLogWriter.Instance.LogWarning("ConsoleLogWriter - warning");
            ConsoleLogWriter.Instance.LogError("ConsoleLogWriter - error");

            FileLogWriter.Instance.LogInfo("FileLogWriter - info");
            FileLogWriter.Instance.LogWarning("FileLogWriter - warning");
            FileLogWriter.Instance.LogError("FileLogWriter - error");
        }
    }
}
