using System;

namespace L10HW
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[3];

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Person {i}");
                Console.Write("Enter name: ");
                string inputName;
                do
                {
                    inputName = Console.ReadLine();
                    if (inputName == null || inputName == string.Empty)
                    {
                        Console.Write("No name specified, please re-enter: ");
                        continue;
                    }

                    break;
                } while (true);

                Console.Write("Enter age: ");
                uint inputAge;
                do
                {
                    if (!uint.TryParse(Console.ReadLine(), out inputAge))
                    {
                        Console.Write("Incorrect age, please re-enter: ");
                        continue;
                    }

                    break;
                } while (true);

                persons[i] = new Person(inputName, inputAge);
                Console.WriteLine();
            }

            foreach (var person in persons)
            {
                Console.WriteLine(person.Description);
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
