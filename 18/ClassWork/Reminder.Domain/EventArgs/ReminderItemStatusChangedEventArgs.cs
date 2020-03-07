using Reminder.Domain.Model;
using System;

namespace Reminder.Domain.EventArgs
{
    public class ReminderItemStatusChangedEventArgs : System.EventArgs
    {
        public ReminderItemStatusChangedModel Reminder { get; set; }

        public ReminderItemStatusChangedEventArgs(ReminderItemStatusChangedModel reminder)
        {
            Reminder = reminder;
        }
    }
}
