using System;
using System.Threading;

namespace L11HW
{
    class Program
    {
        static void Main(string[] args)
        {
            ReminderItem reminderItem = new ReminderItem(DateTimeOffset.Now + TimeSpan.FromDays(2.3));
            reminderItem.WriteProperties();
            Console.WriteLine();

            ReminderItem reminderItem1 = new ReminderItem(DateTimeOffset.Now + TimeSpan.FromDays(-20.5), "Negative");
            reminderItem1.WriteProperties();
            Console.WriteLine();

            ReminderItem reminderItem2 = new ReminderItem(DateTimeOffset.Now + TimeSpan.Zero, "Zero");
            reminderItem2.WriteProperties();
            Console.WriteLine();

            Console.WriteLine("Delay 3 seconds");
            //Thread.Sleep(3000);

            reminderItem1.AlarmMessage = null;
            reminderItem1.WriteProperties();
            Console.WriteLine();

            Console.WriteLine($"Total reminders: {ReminderItem.ReminderCount}");
            Console.WriteLine();

            ReminderReflectionConsolePresenter.PrintMembers(reminderItem);



            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
