using System;
using System.Collections.Generic;
using System.Text;

namespace L12HW
{
    public class PhoneReminderItem : ReminderItem
    {
        public string PhoneNumber { get; set; }

        public PhoneReminderItem(string phoneNumber,
                                 DateTimeOffset alarmDate,
                                 string alarmMessage = null) : base(alarmDate, alarmMessage)
        {
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        }

        public override string ToString()
        {
            return $"Reminder for phone number: {PhoneNumber}\n" + base.ToString();
        }
    }
}
