using System;
using static System.Console;
using System.Globalization;
using System.Threading;
using System.Text;
using L03_ClassWork.Properties;

namespace L03_ClassWork
{
    class Program
    {
        static void Main(string[] args)
        {
 //           object s = 10;
 //           s = (int) s + 5;
 //           WriteLine(s);

            if (args != null && args.Length > 0)
            {
                Thread.CurrentThread.CurrentCulture
                    = Thread.CurrentThread.CurrentUICulture
                        = CultureInfo.GetCultureInfo(args[0]);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            }

            //Console.InputEncoding = Encoding.Unicode;
            Console.InputEncoding = new UTF8Encoding(false);
            Console.OutputEncoding = Encoding.Unicode;

            //Write("Введите вещественное число: ");
            Write(Resources.NumberPrompt);
            string input = ReadLine().Trim();
            WriteLine("\nВведено: " + input);
            double number = Double.Parse(input);

            WriteLine(Resources.Result + number*number);

        }
    }
}

