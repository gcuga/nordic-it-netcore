using System;
using System.Text;

namespace L07_HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Самостоятельная работа *****\n");
            task0();

            Console.WriteLine("\n\n***** Задание 1 *****\n");
            Task1();

            Console.WriteLine("\n\n***** Задание 2 *****\n");
            Task2();

            Console.WriteLine("\nНажмите любую клавишу для выхода…");
            Console.ReadKey();
        }

        //  Дана строка с “грязными” пробелами:
        //    string text = "   lorem    ipsum    dolor      sit   amet ";
        //  Тут только первая часть от предыдущего задания, но сделать ее надо перебором каждого
        //  символа исходной строки(в цикле for или foreach) и “набора” результирующей строки в
        //  StringBuilder: Необходимо произвести над ней следующие операции:
        //    ● “Очистить” исходную строку от лишних пробелов в начале, в конце строки, а также между
        //      словами, а также поднять регистр второго слова:
        //      ○ “ lorem ipsum dolor sit amet ” → “lorem IPSUM dolor sit amet”
        public static void task0()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            string text = "   lorem    ipsum    dolor     sit   amet     ";
            StringBuilder sb = new StringBuilder(string.Empty, text.Length);
            Console.WriteLine(text);

            int wordCounter = 0;
            bool spaceFlag = false;
            const char spaceChar = ' ';

            foreach (char item in text)
            {
                if (spaceFlag && item == spaceChar)
                {
                    // если текущий символ пробел и предыдущий символ был пробелом
                    continue;
                }
                else if (wordCounter == 0 && item == spaceChar)
                {
                    // пробелы перед первым словом
                    continue;
                }

                if ((spaceFlag || wordCounter == 0) && item != spaceChar)
                {
                    // начало нового слова, если предыдущий символ был пробелом или слов еще не было
                    wordCounter++;
                }

                sb.Append((wordCounter == 2 && item != spaceChar) ? char.ToUpper(item) : item);
                spaceFlag = (item == spaceChar);
            }

            if (sb[sb.Length - 1] == spaceChar)
            {
                // удаляем последний символ если он пробел
                sb.Remove(sb.Length - 1, 1);
            }

            text = sb.ToString();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(text);
            Console.ResetColor();
            Console.WriteLine();
        }

        //  Написать консольное приложение, которое запрашивает строку и выводит количество слов, начинающихся на букву A.
        //  Программа должна спрашивать исходную строку до тех пор, пока пользователь не введёт хотя бы 2 слова.
        //  Пример работы программы:
        //  > Введите строку из нескольких слов:
        //  > тест /это ввод пользователя/
        //  > Слишком мало слов :(Попробуйте ещё раз:
        //  > Антон купил арбуз.Алый мак растет среди травы. /это ввод пользователя/
        //  > Количество слов, начинающихся с буквы 'А': 3.
        public static void Task1()
        {
            const char firstChar = '\u0410';  // А
            char[] separator = { ' ', '.' };
            string input;
            string[] words;
            int counter = 0;

            Console.WriteLine("Введите строку из нескольких слов:");
            do
            {
                input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Строка из внешнего источника не получена!");
                    return;
                }

                words = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (words == null || words.Length < 2)
                {
                    Console.WriteLine("Слишком мало слов :(Попробуйте ещё раз:");
                    continue;
                }

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] != null && char.ToUpper(words[i][0]) == firstChar)
                    {
                        counter++;
                    }
                }

                break;
            } while (true);

            Console.WriteLine($"Количество слов, начинающихся с буквы '{firstChar}': {counter}");
        }

        //  Написать консольное приложение, которое запрашивает строку,
        //  а затем выводит все буквы приведенные к нижнему регистру в обратном порядке.
        //  Программа должна спрашивать исходную строку до тех пор,
        //  пока пользователь не введет строку, содержащую печатные символы.
        //  Пример работы программы:
        //  > Введите непустую строку:
        //  >      /это ввод пользователя/
        //  > Вы ввели пустую строку :(Попробуйте ещё раз:
        //  > Не до логики, голоден /это ввод пользователя/
        //  > недолог ,икигол од ен
        public static void Task2()
        {
            string input;
            Console.WriteLine("Введите строку:");
            do
            {
                input = Console.ReadLine();
                if (input == null)
                {
                    Console.WriteLine("Строка из внешнего источника не получена!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Вы ввели пустую строку :(Попробуйте ещё раз:");
                    continue;
                }
                break;
            } while (true);

            input = input.ToLower();
            char[] arr = input.Trim().ToCharArray();
            Array.Reverse(arr);
            string output = new string(arr);
            Console.WriteLine(output);
        }
    }
}
