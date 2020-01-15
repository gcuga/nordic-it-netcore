using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace L04_ClassWork
{
    public static class Class1
    {
        public static void exercise1()
        {
            int i = 0;
            i++;
            Console.WriteLine("i = " + i);

            int j = i++;
            Console.WriteLine("i = {0}, j = {1}", i, j);
            // i = 2, j = 1

            int a = 18;
            int b = a++;
            Console.WriteLine(a == b);
            Console.WriteLine(a != b);
            Console.WriteLine(a > b);
            Console.WriteLine(a < b);
            Console.WriteLine(a >= b);
            Console.WriteLine(a <= b);
        }

        public static void exercise2()
        {
            double a, h;
            int n;

            Console.Write("Введите a:");
            a = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Введите h:");
            h = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Введите n:");
            n = int.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            double v = (h * n * a * a) / (12 * Math.Tan(Math.PI / n));
            double v1 = a / (6 * v);

            double sFull = n * a * (v1 + Math.Sqrt(h * h + Math.Pow(v1, 2))) / 2;

            double sSide = n * a * (h * h + Math.Sqrt(h * h + Math.Pow(v1, 2))) / 2;

            Console.WriteLine("v = {0}, sFull = {1}, sSide = {2}", v, sFull, sSide);
        }

        enum Day
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }

        [Flags]
        enum Pigs
        {
            NifNif = 0b0001,
            NufNuf = 0b0010,
            NafNaf = 0b0100
        }

        enum Season
        {
            Winter = 3,
            Spring = 6,
            Summer = 9,
            Autumn = 12
        }

        public static void exercise3()
        {
            //Pigs theFirstPig = Pigs.NifNif;

            //Pigs theSecondPig = (Pigs)Enum.Parse(typeof(Pigs), Console.ReadLine());

            byte a = 0b01001010;
            byte b = 0b01001100;

            Console.WriteLine(Convert.ToString(a & b, 2).PadLeft(8, '0'));
            Console.WriteLine(Convert.ToString(a | b, 2).PadLeft(8, '0'));
            Console.WriteLine(Convert.ToString(a ^ b, 2).PadLeft(8, '0'));

            Pigs theFirstGroup = Pigs.NifNif | Pigs.NafNaf;  // 5 = 0b0101

            Pigs allPigs = Pigs.NifNif | Pigs.NufNuf | Pigs.NafNaf;

            Pigs notInGroup = allPigs ^ theFirstGroup;

            Console.WriteLine(notInGroup);
        }

        [Flags]
        enum Colors
        {
            Red       = 0b000000000001,
            Orange    = 0b000000000010,
            Yellow    = 0b000000000100,
            Green     = 0b000000001000,
            SkyBlue   = 0b000000010000,
            Blue      = 0b000000100000,
            Violet    = 0b000001000000,
            Tan       = 0b000010000000,
            White     = 0b000100000000,
        }

        public static void exercise4()
        {
            Colors favoriteColors = 0;

            for (int i = 1; i <= 4; i++ )
            {
                Console.Write("Введите любимый цвет {0}: ", i);
                Colors inputColor = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
                favoriteColors |= inputColor;
            }

            Console.WriteLine("Избранные цвета: " + favoriteColors);
            Console.WriteLine("Нелюбимые цвета: " + ((Colors) 0b000111111111 ^ favoriteColors));
        }
    }
}
