using System;
using System.Globalization;

namespace L07_ClassWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //Challenge1();
            Challenge2();
        }

        public static void Challenge1()
        {
            Console.Write("Введите первое число: ");
            double a = double.Parse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture);

            Console.Write("Введите второе число: ");
            double b = double.Parse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture);

            Console.WriteLine(Math.Round(a, 3) + " * " + Math.Round(b, 3) + " = " + Math.Round(a * b, 3));
            Console.WriteLine(string.Format("{0:0.###} + {1:0.###} = {2:0.###}", a, b, a + b));
            Console.WriteLine($"{a:0.###} - {b:0.###} = {a - b:0.###}");
        }

        public static void Challenge2()
        {
            Console.Write("Введите строку: ");
            string s = Console.ReadLine();
            Console.Write("Введите подстроку для поиска: ");
            string searchString = Console.ReadLine();
            string result = string.Empty;
            int place = s.IndexOf(searchString, 0, StringComparison.OrdinalIgnoreCase);

            if (place < 0)
            {
                Console.WriteLine($"Подстрока {searchString} не найдена в строке {s}");
            }
            else
            {
                result = "Индексы подстроки в строке: ";
                do
                {
                    result += place + ",";
                    place = s.IndexOf(searchString, place + 1, StringComparison.OrdinalIgnoreCase);
                } while (place >= 0);
                Console.WriteLine(result.TrimEnd(','));
            }
        }

        public static void Challenge3()
        {
            Console.Write("Введите строку: ");
            string s = Console.ReadLine();

        }
    }
}
