using System;
using System.Diagnostics;

namespace L09CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercise1();
        }

        public static void Exercise1()
        {
            var arr = GetArray(50000, 1000);
            var arr1 = (int[]) arr.Clone();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Console.WriteLine("Bubble sort");
            //PrintArray(arr);
            BubbleSort(arr);
            //PrintArray(arr);
            sw.Stop();
            Console.WriteLine($"Timer: {sw.ElapsedMilliseconds}");

            sw.Reset();
            sw.Start();
            Console.WriteLine("\nMerge sort");
            //PrintArray(arr1);
            Array.Sort(arr1);
            //PrintArray(arr1);
            sw.Stop();
            Console.WriteLine($"Timer: {sw.ElapsedMilliseconds}");

            static int[] GetArray(int length, int maxValue)
            {
                var arr = new int[length]; // создаем массив с размером равным length
                var rnd = new Random(); // создаем объект генератора случайных чисел
                for (var i = 0; i < arr.Length; i++) // перебираем каждый элемент массива
                {
                    arr[i] = rnd.Next(maxValue); // заполняем его произвольным значением
                }

                return arr;
            }

            static void BubbleSort(int[] arr)
            {
                int temp;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[i] > arr[j])
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }
            }

            static void PrintArray(int[] array)
            {
                Console.WriteLine("==============================");
                for (var i = 0; i < array.Length; i++)
                {
                    Console.WriteLine(array[i]);
                }
            }
        }
    }
}
