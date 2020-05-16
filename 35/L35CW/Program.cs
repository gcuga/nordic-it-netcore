using System;
using System.Collections.Generic;
using System.Linq;

namespace L35CW
{
    class Program
    {
        static void Main(string[] args)
        {




        }

        public static void Example1()
        {
            List<Person> persons = new List<Person>
            {
                new Person { FirstName = "Andrey" },
                new Person { FirstName = "Anton" }
            };

            var s = persons.Where(x => x.FirstName.StartsWith("An"));

            s.ToList().ForEach(x => Console.WriteLine(x.FirstName));
        }

    }



    class Person
    {
        public string FirstName { get; set; }
    }

}
