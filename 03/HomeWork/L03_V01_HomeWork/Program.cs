using System;

namespace L03_V01_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // тестирование класса BaseMultiplicationTable
            Console.WriteLine("Тестирование класса BaseMultiplicationTable:\n");
            int[] row = null;
            int[] column = null;
            try
            {
                BaseMultiplicationTable t1 = new BaseMultiplicationTable(row, column);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }
            Console.WriteLine();

            int[] row2 = { int.MinValue};
            int[] column2 = { 2 };
            try
            {
                BaseMultiplicationTable t2 = new BaseMultiplicationTable(row2, column2);
            }
            catch (System.OverflowException)
            {
                Console.WriteLine("Multiplication overflow");
            }
            Console.WriteLine();

            int[] row3 = { 23, -3455, 6, -8 };
            int[] column3 = { 3, 2, 6, -5, 10, 7 };
            BaseMultiplicationTable t3 = new BaseMultiplicationTable(row3, column3);
            Console.WriteLine(t3.ToString(10));
            Console.WriteLine();
            Console.WriteLine(t3.ToString());
            Console.WriteLine();

            int[] row4 = { 3, 2, 6, -5, 10, 7 };
            int[] column4 = { 23, -3455, 6, -8 };
            BaseMultiplicationTable t4 = new BaseMultiplicationTable(row4, column4);
            Console.WriteLine(t4.ToString());
            Console.WriteLine();

            // тестирование класса PythagoreanTable
            Console.WriteLine("Тестирование класса PythagoreanTable:\n");
            PythagoreanTable pythagorean1 = new PythagoreanTable();
            Console.WriteLine(pythagorean1.ToString());
            Console.WriteLine();
            Console.WriteLine(pythagorean1.ToString(7));
            Console.WriteLine();

            PythagoreanTable pythagorean2 = new PythagoreanTable(5, 12);
            Console.WriteLine(pythagorean2.ToString());
            Console.WriteLine();

            try
            {
                PythagoreanTable pythagorean3 = new PythagoreanTable(25, 9);
                //PythagoreanTable pythagorean3 = new PythagoreanTable(0, 9);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("{0}", e.Message);
            }
        }
    }
}
