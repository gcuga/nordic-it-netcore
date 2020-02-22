using System;
using System.Collections.Generic;
using System.Text;

namespace L15CW
{
    class Account<T>
    {
        public T Id { get; set; }
        public string Name { get; private set; }

        public Account(T id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void WriteProperties()
        {
            Console.WriteLine($"{nameof(Id)} = {Id}, {nameof(Name)} = {Name}");
        }
    }
}


//Реализовать класс Account
//● Два свойства:
//○ int Id { get, private set },
//○ string Name { get, private set },
//● Конструктор с параметрами для их заполнения
//● Публичный метод void WriteProperties(), выводящий параметры в
//консоль.
//Затем сделать класс обобщённым, обобщяя его по типу свойства Id.
//В основном потоке программы создать несколько экземпляров класса
//Account с различными типами данных для Id: int, string, Guid.Для всех
//вызвать метод WriteProperties.