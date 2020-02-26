using Calculator.Operation;
using Calculator.Figure;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle(1.5);
            Func<double, double> func1 = ShapeMath.Perimeter;
            Func<double, double> func2 = ShapeMath.Area;
            Console.WriteLine($"Perimeter is {circle.Calculate(func1):0.00}");
            Console.WriteLine($"Area is {circle.Calculate(func2):0.00}");
        }
    }
}
