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

        }
    }
}
