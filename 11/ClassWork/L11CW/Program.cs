using System;

namespace L11CW
{
    class Program
    {
        static void Main(string[] args)
        {
            Pet pet1 = new Pet();
            pet1.Kind = Pet.AnimalKind.Dog;
            pet1.Name = "Rex";
            pet1.Sex = Pet.SexType.M;
            pet1.DateOfBirth = new DateTimeOffset( 2018, 3, 5, 0, 0, 0, TimeSpan.Zero);
            pet1.SetBirthPlace("Деревня гадюкино");
            pet1.WriteDescription(true);
            pet1.WriteDescription();

            Pet pet2 = new Pet("Нерезиновая", Pet.AnimalKind.Mouse, "Mikki",
                               Pet.SexType.M, DateTimeOffset.Now - TimeSpan.FromDays(250));
            pet2.WriteDescription(true);
            pet2.WriteDescription();


            ReminderItem reminderItem1 =
                new ReminderItem(DateTimeOffset.Now + TimeSpan.FromMinutes(-10), null);
            reminderItem1.WriteProperties();
        }
    }
}
