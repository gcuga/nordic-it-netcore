using System;
using System.Globalization;

namespace ClassWork02
{
    class Program
    {
        static void Main(string[] args)
        {
            int maximumTemperatureLevel = 39;
            Console.WriteLine("maximumTemperatureLevel = " + maximumTemperatureLevel);

            string myNumberAsText = "4.5";

            float number = float.Parse(myNumberAsText, CultureInfo.InvariantCulture);

            Console.WriteLine(-number);

            for (int i = 1; i < 5; i++) {
                Console.WriteLine(int.MaxValue + i);
            }

            string radius;
            Console.WriteLine("Введите радиус:");
            radius = Console.ReadLine().Replace(',', '.');

            Console.WriteLine("Длина окружности = " +
                    (double.Parse(radius, CultureInfo.InvariantCulture) * 2D * Math.PI) );

            
        }
    }
}
