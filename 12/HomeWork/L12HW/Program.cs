using System;
using System.Collections.Generic;

namespace L12HW
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ReminderItem> reminderList = new List<ReminderItem>
            {
                new ReminderItem(DateTimeOffset.Now + TimeSpan.FromDays(1), "Base message"),
                new PhoneReminderItem("+7 (495) 223-33-22",
                                      DateTimeOffset.Now - TimeSpan.FromDays(1.545),
                                      "Message for phone"),
                new ChatReminderItem("Slack",
                                     "Slackbot",
                                     DateTimeOffset.Now + TimeSpan.FromDays(2.2),
                                     "Message for chat")
            };

            foreach (var item in reminderList)
            {
                item.WriteProperties();
                Console.WriteLine("================================================");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
