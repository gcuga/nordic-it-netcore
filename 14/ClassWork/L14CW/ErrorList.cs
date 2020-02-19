using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace L14CW
{
    class ErrorList :IDisposable, IEnumerable<string>
    {
        public static string OutputPrefixFormat { get; set; }

        public string Category { get; }

        private List<string> _errors { get; set; }

        static ErrorList()
        {
            OutputPrefixFormat = @"MMMM d, yyyy (h:mm tt)"; //April 7, 2019 (7:55 PM)
        }

        public ErrorList(string category)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            _errors = new List<string>();
        }

        public void WriteToConsole()
        {
            foreach (var item in _errors)
            {
                Console.WriteLine($"{DateTimeOffset.Now.ToString(OutputPrefixFormat, CultureInfo.InvariantCulture)} " +
                    $"{Category}: {item}");
            }
        }

        public void Dispose()
        {
            _errors?.Clear();
            _errors = null;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)_errors).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)_errors).GetEnumerator();
        }

        public void Add(string errorMessage)
        {
            _errors.Add(errorMessage);
        }


    }
}


//Написать класс ErrorList, который будет хранить тип ошибок определённой
//категории, реализующий интерфейс IDisposable.Он должен содержать
//следующие публичные члены:
//● Свойство Category типа string - категория ошибок, свойство read-only, задаётся
//из конструктора,
//● Свойство Errors типа List<string> - собственно список ошибок,
//● Конструктор, в котором будут инициализироваться свойство Category (через
//параметр category) и пустой список строк Errors,
//● Метод void Dispose(), в котором будет происходить 1) очистка списка методом
//Clear() и 2) приравнивание свойства Errors к null.
//В основном потоке программы создать экземпляр объекта ErrorList
//используя конструкцию using, и написать пару ошибок.
//Затем вывести их на экран в формате: “категория: ошибка”.