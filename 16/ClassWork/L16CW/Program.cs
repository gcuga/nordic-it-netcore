using System;

namespace L16CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle(1.5);
            Func<double, double> func = ShapeMath.Perimeter;
            Console.WriteLine($"Perimeter is {circle.Calculate(func):0.00}");
            func = ShapeMath.Area;
            Console.WriteLine($"Area is {circle.Calculate(func):0.00}");

            Console.WriteLine("================================");
            Func<double, double> func1 = radius => 2 * Math.PI * radius;
            Func<double, double> func2 = radius => Math.PI * radius * radius;
            Console.WriteLine($"Perimeter is {circle.Calculate(func1):0.00}");
            Console.WriteLine($"Area is {circle.Calculate(func2):0.00}");



        }
    }
}







// https://docs.oracle.com/javase/tutorial/java/IandI/override.html