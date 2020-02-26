using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Operation
{
    public static class ShapeMath
    {
        public static double Perimeter(double radius)
        {
            return 2 * Math.PI * radius;
        }

        public static double Area(double radius)
        {
            return Math.PI * radius * radius;
        }
    }
}
