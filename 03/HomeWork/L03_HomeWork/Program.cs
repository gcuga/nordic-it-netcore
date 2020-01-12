using System;

namespace L03_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiplicationTable t1 = new MultiplicationTable();
            t1.PrintTable();
            Console.WriteLine("");
            int[,] t1arr = t1.Table;
            t1arr[1, 1] = 1000;
            t1.PrintTable();

            Console.WriteLine("");
            int[] row = { 12, 4, 564, 300 };
            int[] col = { 2, 5, 200, 10, 1, 2, 3, 5, 33 };
            MultiplicationTable t2 = new MultiplicationTable(row, col);
            t2.PrintTable();
            Console.WriteLine("");


            MultiplicationTable t3 = new MultiplicationTable(rangeFormat: RangeFormat.Quantity, number1: 7, number2: 5);
            t3.PrintTable();
            Console.WriteLine("");


        }
    }

}
