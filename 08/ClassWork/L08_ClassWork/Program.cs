using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace L08_ClassWork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exercise1();
            // Exercise2();
            // Exercise3();
            // Exercise4();
            Exercise5();
        }

        public static void Exercise1()
        {
            var intList = new List<int>();
            intList.Add(10);
            intList.Add(20);
            intList.Add(30);
            intList.Add(40);
            Console.WriteLine(string.Join(separator: ", ", intList));

            var listOfStrings = new List<string>();
            listOfStrings.Add("One");
            listOfStrings.Add("Two");
            listOfStrings.Add("Three");
            listOfStrings.Add(null);
            listOfStrings.Add("Five");
            Console.WriteLine(string.Join(separator: ", ", listOfStrings));

            List<int> arrayList = new List<int>
            {
                0,
                1,
                1,
                2
            };

            arrayList.RemoveAll((int i) => i == 1);
            Console.WriteLine(string.Join(separator: ", ", arrayList));
        }

        public static void Exercise2()
        {
            double number;
            List<double> listOfDouble = new List<double>();
            Console.WriteLine("Введите вещественные числа, для расчета введите stop: ");
            do
            {
                string input = Console.ReadLine();
                if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out number)) 
                {
                    listOfDouble.Add(number);
                    continue;
                }
                else if (input == "stop")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Вы ввели неверное число, попробуйте еще раз");
                }
            } while (true);

            int counter = 0;
            double sum = 0;
            foreach (var item in listOfDouble)
            {
                counter++;
                sum += item;
            }

            Console.WriteLine($"Сумма {sum} среднее арифметическое {Math.Round(sum/counter, 2, MidpointRounding.AwayFromZero)}");
        }

        public static void Exercise3()
        {
            var countries = new Dictionary<string, string>
            {
                { "Moscow", "Russia" },
                { "London", "Great Britain" },
                { "Washington", "USA"},
                { "Paris", "France"},
                { "Beijing", "China" },
                { "Kabul", "Afghanistan"}
            };

            var rand = new Random();
            do
            {
                int element = rand.Next(0, countries.Count);
                KeyValuePair<string, string> kvp = countries.ElementAt(element);

                Console.WriteLine("Введите столицу страны - " + kvp.Value + " :");
                string input = Console.ReadLine();



            } while (true);


        }

        public static void Exercise4()
        {
            Queue<string> numbers = new Queue<string>();
            numbers.Enqueue("one");
            numbers.Enqueue("two");
            numbers.Enqueue("three");
            numbers.Enqueue("four");
            numbers.Enqueue("five");

            foreach (string item in numbers)
            {
                Console.WriteLine(item);
            }

            Queue<int> queue = new Queue<int>();
            Console.WriteLine("Задайте задания, run - обработать задания, exit - выход:");
            string input;
            int number;
            do
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out number))
                {
                    queue.Enqueue(number);
                }
                else if (input == "run")
                {
                    int count = queue.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine($"Квадратный корень {Math.Sqrt(queue.Dequeue())}");
                    }
                }
                else if (input == "exit")
                {
                    foreach (int item in queue)
                    {
                        Console.WriteLine($"Необработан элемент {item}");
                    }
                    break;
                }
            } while (true);
        }

        public static void Exercise5()
        {
            string input;
            int plateCounter = 0;
            Stack<string> stack = new Stack<string>();
            Console.WriteLine("Вводите команды, список команд – wash, dry, или exit");
            do
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }

                if (input == "wash")
                {
                    plateCounter++;
                    stack.Push($"Plate {plateCounter}");
                }

                if (input == "dry")
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        Console.WriteLine("“Стопка тарелок пуста!”");
                    }
                }

                if (stack.Count > 0)
                {
                    Console.WriteLine($"Тарелок в стопке на вытирание: {stack.Count}");
                }
            } while (true);

            Console.WriteLine("Остались в стопке:");
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Всего тарелок было помещено в стопку: {plateCounter}");
        }
    }
}
