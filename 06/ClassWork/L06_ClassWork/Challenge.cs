using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace L06_ClassWork
{
    class Challenge
    {
        public static void С1()
        {
            do
            {
                Console.Write("Введите слово:");
                if (Console.ReadLine() == "Exit")
                {
                    break;
                }
            } while (true);
        }

        public static void С2()
        {
            int i = 0;
            do
            {
                Console.Write("Введите число:");
                if (!int.TryParse(Console.ReadLine(), out int inputNumber))
                {
                    i++;
                    if (i <= 1)
                    {
                        Thread.Sleep(1000);
                    }
                    else if (i <= 2)
                    {
                        Thread.Sleep(2000);
                    }
                    else if (i <= 3)
                    {
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        break;
                    }
                    continue;
                }

                Console.WriteLine($"Вы ввели {inputNumber}");
                break;
            } while (true);
        }

        public static void С3()
        {
            int[][] schoolRecords =
            {
                new int[] { 2, 3, 3, 2, 3},
                new int[] { 2, 4, 5, 3},
                null,
                new int[] { 5, 5, 5, 5},
                new int[] { 4 }
            };

            double averageMarkAll = 0;
            double countMark = 0;
            for (int i = 0; i < schoolRecords.GetLength(0); i++)
            {
                if (schoolRecords[i] == null)
                {
                    Console.WriteLine($"The average mark for day {i} is N/A");
                    continue;
                }
                
                double averageMark = 0;
                for (int j = 0; j < schoolRecords[i].GetLength(0); j++)
                {
                    averageMark += schoolRecords[i][j];
                    countMark++;
                }
                averageMarkAll += averageMark;
                averageMark /= schoolRecords[i].GetLength(0);
                Console.WriteLine($"The average mark for day {i} is {averageMark:0.0}");
            }
            averageMarkAll /= countMark;
            Console.WriteLine(
                $"The average mark for all the week is {Math.Round(averageMarkAll, 1,  MidpointRounding.AwayFromZero):0.0}");
        }
    }
}
