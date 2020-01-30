using System;
using System.Collections.Generic;

namespace L08HW
{
    class Program
    {
        static void Main(string[] args)
        {
            var testStrings = new List<string>
            {
                "()",
                "[]()",
                "[[]()]",
                "([([])])()[]",
                null,
                "(",
                "[][)",
                "[(])",
                "(()[]]",
                "]",
                "some string",
                string.Empty
            };

            foreach (var item in testStrings)
            {
                try
                {
                    Console.WriteLine($"{TryParseBrackets(item)}: \"{item}\"");
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }

        public static bool TryParseBrackets(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(paramName: nameof(input), message: "Input string cannot be null");
            }

            // виды скобок
            const char parenthesisOpen = '(';
            const char parenthesisClose = ')';
            const char squareBracketsOpen = '[';
            const char squareBracketsClose = ']';

            // объявляем стек в котором будем хранить открытые на текущий момент скобки
            // однако записывать в стек для удобства будем обратное состояние,
            // т.е. соответствующую закрывающую скобку
            var stackOfBrackets = new Stack<char>();

            foreach (char item in input)
            {
                if (stackOfBrackets.Count > 0 && item == stackOfBrackets.Peek())
                {
                    // закрывающая скобка соответствует ожидаемой по стеку, удаляем из стека
                    stackOfBrackets.Pop();
                }
                else if (item == parenthesisOpen)
                {
                    // открывающая круглая скобка, добавляем в стек закрывающую круглую скобку
                    stackOfBrackets.Push(parenthesisClose);
                }
                else if (item == squareBracketsOpen)
                {
                    // открывающая квадратная скобка, добавляем в стек закрывающую квадратную скобку
                    stackOfBrackets.Push(squareBracketsClose);
                }
                else if (item == parenthesisClose ||
                         item == squareBracketsClose)
                {
                    // закрывающая скобка не соответствует ожидаемой
                    return false;
                }
            }

            if (stackOfBrackets.Count == 0)
            {
                // либо скобок не было либо все открывающие имеют соответствующие закрывающие
                return true;
            }

            return false;
        }
    }
}