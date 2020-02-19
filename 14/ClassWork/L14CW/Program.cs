using System;
using System.IO;

namespace L14CW
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LogFileWriter(@"C:\temp\L14CW\log.txt");

            //Console.WriteLine(logger.FileName);
            //Console.WriteLine(File.Exists(logger.FileName));
            //Console.WriteLine(Path.GetDirectoryName(logger.FileName));

            logger.WriteLog(Path.GetFullPath(logger.FileName));

            var logger_ext = new LogFileWriterExtended(@"C:\temp\L14CW\log_extended.txt");
            logger_ext.WriteLog(Path.GetFullPath(logger_ext.FileName));
            logger_ext.Dispose();

            using (var logger_using = new LogFileWriterExtended(@"C:\temp\L14CW\log_using.txt"))
            {
                logger_using.WriteLog(Path.GetFullPath(logger_using.FileName));
            }


            using (var errList = new ErrorList("Message"))
            {
                errList.Add("сообщение 1");
                errList.Add("сообщение 2");
                errList.Add("сообщение 3");
                errList.WriteToConsole();
            }


            uint mask  = 0b_00000000_00000000_00000000_11111111;
            uint input = 0b_11111111_00011000_00011000_00000000;
            uint res = input & mask;
            uint invert = res ^ 0b_00000000_00000000_00000000_11111111;

            Console.WriteLine(
                $"{nameof(mask).PadRight(6)}: {Convert.ToString(mask, 2).PadLeft(32, '0')} - {mask}" +
                $"\n{nameof(input).PadRight(6)}: {Convert.ToString(input, 2).PadLeft(32, '0')} - {input}" +
                $"\n{nameof(res).PadRight(6)}: {Convert.ToString(res, 2).PadLeft(32, '0')} - {res}" +
                $"\n{nameof(invert).PadRight(6)}: {Convert.ToString(invert, 2).PadLeft(32, '0')} - {invert}");
        }
    }
}
