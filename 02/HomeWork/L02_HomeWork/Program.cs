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
            bool quit;
            double number1, number2;
            CalculatorOperation operation;
            string resultString;
            do {
                Console.WriteLine("\nВведите первое число:");
                number1 = ReadNumber();
                operation = ReadOperation();
                if (operation == CalculatorOperation.Power) {
                    Console.WriteLine("\nВведите степень:");
                } else {
                    Console.WriteLine("\nВведите второе число:");
                }
                number2 = ReadNumber();
                // проверка на допустимость чисел и операции
                if ((operation == CalculatorOperation.Division ||
                     operation == CalculatorOperation.Remainder
                     ) && number2 == 0) {
                    resultString = "Деление на ноль запрещено";
                }
                else if (operation == CalculatorOperation.Power &&
                         !(
                           number1 > 0 ||
                           (number1 == 0 && number2 > 0) ||
                           ((double)((long)number2) == number2 && number2 > 0)
                          )) {
                    resultString = "Не выполнены условия при которых возведение в степень имеет смысл:" +
                                   "\n 1) первое число > 0" +
                                   "\n 2) первое число = 0, второе число > 0" +
                                   "\n 3) второе число натуральное > 0";
                }
                else {
                    Console.Write("\nРезультат вычисления: ");
                    switch (operation)
                    {
                        case CalculatorOperation.Addition:
                            resultString = number1 + " + " + number2 + " = " + (number1 + number2);
                            break;
                        case CalculatorOperation.Subtraction:
                            resultString = number1 + " - " + number2 + " = " + (number1 - number2);
                            break;
                        case CalculatorOperation.Multiplication:
                            resultString = number1 + " * " + number2 + " = " + (number1 * number2);
                            break;
                        case CalculatorOperation.Division:
                            resultString = number1 + " / " + number2 + " = " + (number1 / number2);
                            break;
                        case CalculatorOperation.Remainder:
                            resultString = number1 + " % " + number2 + " = " + (number1 % number2);
                            break;
                        case CalculatorOperation.Power:
                            resultString = number1 + " ^ " + number2 + " = " + Math.Pow(number1, number2);
                            break;
                        default:
                            resultString = "Не задан тип операции";
                            break;
                    }
                }
                Console.WriteLine(resultString);
                Console.WriteLine("\nЗавершить работу калькулятора - Q, продолжить - Enter:");
                quit = (Console.ReadLine().ToLower() == "q");
            } while (!quit);
        }

        private static double ReadNumber()
        {
            double number;
            bool tryParse; 
            do {
                tryParse = double.TryParse(Console.ReadLine().Replace(',', '.'),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out number);
                if (!tryParse) {
                    Console.WriteLine("Введенная строка не является числом, введите число:");
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
