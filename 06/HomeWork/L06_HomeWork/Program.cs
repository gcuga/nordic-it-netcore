using System;

namespace L06_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Задание 1 *****\n");
            EvenDigitCount();

            Console.WriteLine("\n\n***** Задание 2 *****\n");
            CumulativeInterest();

        }

        internal static void EvenDigitCount()
        {
            Console.WriteLine("Введите положительное натуральное число не более 2 миллиардов:");
            int inputNumber;
            do
            {
                try
                {
                    inputNumber = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"Ошибка {e.GetType()}! Попробуйте ещё раз:");
                    continue;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine($"Ошибка {e.GetType()}! Попробуйте ещё раз:");
                    continue;
                }

                if (inputNumber < 1 || inputNumber > 2_000_000_000)
                {
                    Console.WriteLine("Введено неверное значение! Попробуйте ещё раз:");
                    continue;
                }

                break;
            } while (true);

            int evenCount = 0;
            int tempNumber = inputNumber;
            while (tempNumber > 0)
            {
                if ((tempNumber % 10) % 2 == 0)
                {
                    evenCount++;
                }
                tempNumber /= 10;
            }

            Console.WriteLine($"В числе {inputNumber} содержится следующее количество четных цифр: {evenCount}.");
        }

        internal static void CumulativeInterest()
        {
            Console.Write("Введите сумму первоначального взноса в рублях: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialPayment))
            {
                Console.WriteLine("Неверное значение суммы первоначального взноса");
                return;
            }

            Console.Write("Введите ежедневный процент дохода в виде десятичной дроби: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal dayPercent))
            {
                Console.WriteLine("Неверное значение ежедневного процента дохода");
                return;
            }

            Console.Write("Введите желаемую сумму накопления в рублях: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal desiredAmount))
            {
                Console.WriteLine("Неверное значение желаемой суммы накопления");
                return;
            }

            if (initialPayment >= desiredAmount ||
                initialPayment <= 0 ||
                dayPercent <= 0 ||
                desiredAmount <= 0)
            {
                Console.WriteLine("При заданных условиях вычисление периода накопления не имеет смысла");
                return;
            }

            Console.WriteLine("Приблизительный расчет количества дней для накопления желаемой суммы");
            Console.Write($"log(1+{dayPercent}) ({desiredAmount} / {initialPayment}) = ");
            Console.WriteLine(
                (int)Math.Ceiling(Math.Log((double)(desiredAmount / initialPayment), 1 + (double)dayPercent)));
            Console.WriteLine();

            int dayCount = 0;
            decimal amount = initialPayment;
            decimal dayAdditive;
            do
            {
                dayCount++;
                if (dayCount < 0)
                {
                    Console.WriteLine("Человечество столько не протянет!");
                    return;
                }

                try
                {
                    dayAdditive = decimal.Round(amount * dayPercent, 2, MidpointRounding.ToEven);
                    amount += dayAdditive;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Столько денег нет на всей Земле!");
                    return;
                }

                if (dayAdditive < 0.01m)
                {
                    Console.WriteLine("Процент слишком мал для ежедневной фиксации");
                    return;
                }

                if (amount >= desiredAmount)
                {
                    break;
                }
            } while (true);

            Console.WriteLine("Необходимое количество дней для накопления желаемой суммы" +
                "\nс ежедневной фиксацией процентов по правилам банковского округления: {0}", dayCount);
        }
    }
}
