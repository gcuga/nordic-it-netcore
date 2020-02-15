using System;

namespace L13CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Aircraft aircraft1 = new Helicopter(2000, 6);
            Aircraft aircraft2 = new Plane(12000, 2);

            Console.WriteLine("Heli");
            Console.WriteLine(aircraft1);
            Console.WriteLine();
            Console.WriteLine("Plane");
            Console.WriteLine(aircraft2);
            Console.WriteLine("========================================");

            Console.WriteLine("Heli - набор высоты");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("--");
                aircraft1.TakeUpper(1000);
                Console.WriteLine(aircraft1);
            }

            Console.WriteLine("========================================");
            Console.WriteLine("Plane - набор высоты");
            for (int i = 0; i < 14; i++)
            {
                Console.WriteLine("--");
                aircraft2.TakeUpper(1000);
                Console.WriteLine(aircraft2);
            }

            Console.WriteLine("========================================");
            Console.WriteLine("Heli - снижение");
            for (int i = 0; i < 14; i++)
            {
                try
                {
                    Console.WriteLine("--");
                    aircraft1.TakeLower(1000);
                    Console.WriteLine(aircraft1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }

            Console.WriteLine("========================================");
            Console.WriteLine("Plane - снижение");
            for (int i = 0; i < 14; i++)
            {
                try
                {
                    Console.WriteLine("--");
                    aircraft2.TakeLower(1000);
                    Console.WriteLine(aircraft2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
    }
}
