using System;
using System.Collections.Generic;
using Reminder.Storage.Core;
using Reminder.Storage.InMemory;
using System.Linq;

namespace L18CW
{
    class Program
    {
        static void Main()
        {
            IReminderStorage storage = new InMemoryReminderStorage();
            storage.Add(new ReminderItem(Guid.NewGuid(), DateTimeOffset.Now, "Alarm"));

            List<ReminderItem> list = storage.Get(ReminderItemStatus.Awaiting);

            list.ForEach((x) => { Console.WriteLine(x.ToString()); }) ;
        }
    }
}
