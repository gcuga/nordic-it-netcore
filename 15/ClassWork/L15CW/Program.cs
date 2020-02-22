using System;
using System.Linq;

namespace L15CW
{
    class Program
    {
        delegate int PerformCalculation(int x, int y);
        delegate int PerformCalculationArray(int[] array);

        static void Main(string[] args)
        {
            //int a = 5, b = 10;
            //Console.WriteLine($"a = {a} b = {b}");
            //Swap<int>(ref a, ref b);
            //Console.WriteLine($"a = {a} b = {b}");

            //Calculator.Add(new MyClass(), new MyClass(), out MyClass res);

            //Account<MyClass> a = new Account<MyClass>(new MyClass(), "dhkugurehg");
            //a.WriteProperties();

            var calc = new SimpleCalculator();
            PerformCalculation performCalculation;
            int result;
            performCalculation = calc.Sum;
            result = performCalculation(10, 5);
            Console.WriteLine(result);
            performCalculation = calc.Multiply;
            result = performCalculation(10, 5);
            Console.WriteLine(result);

            int[] array = { 12, 2, -4, 10, 54 };

            //PerformCalculationArray calcArray = Sum;
            Func<int[], int> calcArray = Sum;
            calcArray = Min;
            Console.WriteLine(calcArray(array));
            calcArray = Enumerable.Max;
            Console.WriteLine(calcArray(array));
        }

        public static int Min(int[] array)
        {
            int min_element = int.MaxValue;
            foreach (var i in array)
            {
                if (i < min_element)
                {
                    min_element = i;
                }
            }
            return min_element;
        }

        public static int Sum(int[] array)
        {
            return 0;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

       

    }

    public class MyClass
    {
        int a = 1;
        string b;
        Object o;

        public override string ToString()
        {
            return "My class" + a;
        }
    }
}
