using System;


// https://planetcalc.ru/534/

namespace L10CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Pet pet1 = new Pet();
            pet1.Kind = Pet.AnimalKind.Dog;
            pet1.Name = "Rex";
            pet1.Sex = Pet.SexType.M;
            pet1.Age = 5;
            pet1.SetBirthPlace("Деревня гадюкино");
            Console.WriteLine(pet1.Description);

            Pet pet2 = new Pet();
            pet2.Kind = Pet.AnimalKind.Cat;
            pet2.Name = "Mursik";
            pet2.Sex = Pet.SexType.Unknown;
            pet2.Age = 100500;
            pet2.SetBirthPlace("Hell");
            Console.WriteLine(pet2.Description);
        }
    }
}
