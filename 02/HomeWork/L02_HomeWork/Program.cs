using System;
using System.Globalization;

namespace L02_HomeWork
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Простой калькулятор\n" +
                              "===================");
            double number1 = ReadNumber("\nВведите первое число:");
            CalculatorOperation operation = ReadOperation();
            double number2 = ReadNumber(messageForUser : "\nВведите второе число:",
                                        division : (operation == CalculatorOperation.Division ||
                                                    operation == CalculatorOperation.Remainder));

            Console.Write("\nРезультат вычисления: ");
            switch (operation) {
                case CalculatorOperation.Addition:
                    Console.WriteLine(number1 + " + " + number2 + " = " + (number1 + number2));
                    break;
                case CalculatorOperation.Subtraction:
                    Console.WriteLine(number1 + " - " + number2 + " = " + (number1 - number2));
                    break;
                case CalculatorOperation.Multiplication:
                    Console.WriteLine(number1 + " * " + number2 + " = " + (number1 * number2));
                    break;
                case CalculatorOperation.Division:
                    Console.WriteLine(number1 + " / " + number2 + " = " + (number1 / number2));
                    break;
                case CalculatorOperation.Remainder:
                    Console.WriteLine(number1 + " % " + number2 + " = " + (number1 % number2));
                    break;
                case CalculatorOperation.Power:
                    Console.WriteLine(number1 + " ^ " + number2 + " = " + Math.Pow(number1, number2));
                    break;
                default:
                    Console.WriteLine("Не задан тип операции");
                    break;
            }
        }

        private static double ReadNumber(
                string messageForUser,
                string parseErrorMessage = "Введенная строка не является числом.",
                bool division = false)
        {
            double number;
            bool tryParse; 

            do {
                Console.WriteLine(messageForUser);
                tryParse = double.TryParse(Console.ReadLine().Replace(',', '.'),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out number);
                if (!tryParse) {
                    Console.WriteLine(parseErrorMessage);
                } else if (division && number == 0) {
                    tryParse = false;
                    Console.WriteLine("Деление на ноль запрещено.");
                }
            } while (!tryParse);

            return number;
        }

        private static CalculatorOperation ReadOperation()
        {
            Console.WriteLine("\nСписок операций:");
            Console.WriteLine("1 - сложение");
            Console.WriteLine("2 - вычитание");
            Console.WriteLine("3 - умножение");
            Console.WriteLine("4 - деление");
            Console.WriteLine("5 - остаток от деления");
            Console.WriteLine("6 - возведение в степень");

            int number;
            bool tryParse;
            do {
                Console.WriteLine("\nВведите номер операции:");
                tryParse = int.TryParse(Console.ReadLine(), out number);
                if (!tryParse) {
                    Console.WriteLine("Некорректный ввод.");
                } else if (!Enum.IsDefined(typeof(CalculatorOperation), number)) {
                    tryParse = false;
                    Console.WriteLine("Некорректный номер операции.");
                }
            } while (!tryParse);
            return (CalculatorOperation) number;
        }
    }
}
