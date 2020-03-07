using System;
using System.Collections.Generic;
using Reminder.Storage.Core;
using Reminder.Storage.InMemory;
using Reminder.Domain;
using System.Linq;
using Reminder.Domain.EventArgs;

namespace L18CW
{
    class Program
    {
        static void Main()
        {
            IReminderStorage storage = new InMemoryReminderStorage();
            ((InMemoryReminderStorage) storage).OnAddSuccess = (s, e) => { Console.WriteLine("adding done"); };
            ((InMemoryReminderStorage)storage).RunWhenUpdatingRun = (s, e) => { Console.WriteLine("updating done"); };

            ReminderDomain domain = new ReminderDomain(storage);
            domain.ReminderItemStatusChanged += OnReminderItemReady;


            storage.Add(new ReminderItem(Guid.NewGuid(), DateTimeOffset.Now, "Alarm"));

            List<ReminderItem> list = storage.Get(ReminderItemStatus.Awaiting);

            list.ForEach((x) => { Console.WriteLine(x.ToString()); }) ;

            var item = new ReminderItem(Guid.NewGuid(), DateTimeOffset.Now.AddSeconds(2), "Hello world");
            storage.Add(item);
            ((InMemoryReminderStorage) storage).RunWhenUpdatingRun = (s, e) => { Console.WriteLine("updating done"); };
            ((InMemoryReminderStorage) storage).RunWhenUpdatingRun +=
                ((InMemoryReminderStorage) storage).OnAddSuccess;
            storage.Update(item);

            domain.Run();
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();

        }

        private static void OnReminderItemReady(object sender, ReminderItemStatusChangedEventArgs e)
        {
            Console.WriteLine($"Reminder of contact {e.Reminder.ContactId}" +
                $" has changed status from {e.Reminder.PreviousStatus}" +
                $" to {e.Reminder.Status}");
        }
    }
}
