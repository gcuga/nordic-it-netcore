using System;

namespace L12CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Exercise1();
        }

        internal static void Exercise1()
        {
            //Напишите новую реализацию свойств PropertiesString и метод WriteToConsole() чтобы произвести
            //сокрытие членов базового класса.
            //Создайте по одному экземпляру каждого класса в основном потоке программы, инициализируйте
            //их свойства и выведите их на экран используя метод WriteToConsole() соответствующих классов.
            BaseDocument baseDocument = new BaseDocument(
                docName: "Приходная накладная", docNumber: "1234512345",
                issueDate: DateTimeOffset.Now - TimeSpan.FromDays(10.3));
            baseDocument.WriteConsole();
            Console.WriteLine();

            Passport passport = new Passport(personName: "Джон", country: "USA",
                docNumber: "2323", issueDate: DateTimeOffset.Now - TimeSpan.FromDays(5.2));
            passport.WriteConsole();
            Console.WriteLine("======================================");
            Console.WriteLine();

            Object[] obj = new Object[6];
            obj[0] = baseDocument;
            obj[1] = passport;
            obj[2] = new Passport(personName: "Mark", country: "Canada",
                docNumber: "7777", issueDate: DateTimeOffset.Now - TimeSpan.FromDays(7.1));
            obj[3] = new string("bla bla bla");
            obj[4] = null;
            obj[5] = new BaseDocument(
                docName: "Расходная накладная", docNumber: "68576",
                issueDate: DateTimeOffset.Now + TimeSpan.FromDays(10));

            foreach (var item in obj)
            {
                Console.WriteLine(item);
                Console.WriteLine();
            }

            Console.WriteLine("======================================");

            foreach (var item in obj)
            {
                //if (item != null && item.GetType().Name == nameof(Passport))
                if (item is Passport)
                {
                    ((Passport)item).ChangeIssueDate(DateTimeOffset.Now);
                }
            }

            foreach (var item in obj)
            {
                Console.WriteLine(item??"null");
                Console.WriteLine();
            }
        }
    }
}
