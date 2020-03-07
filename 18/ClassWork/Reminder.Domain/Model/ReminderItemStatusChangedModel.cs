using System;
using Reminder.Storage;
using System.Collections.Generic;
using System.Text;
using Reminder.Storage.Core;

namespace Reminder.Domain.Model
{
    public class ReminderItemStatusChangedModel
    {
        public string ContactId { get; set; }

        public string AlarmMessage { get; set; }

        public DateTimeOffset AlarmDate { get; set; }

        public ReminderItemStatus Status { get; set; }

        public ReminderItemStatus PreviousStatus { get; set; }

        public ReminderItemStatusChangedModel(
            ReminderItem reminderItem,
            ReminderItemStatus previousStatus)
        {
            AlarmDate = reminderItem.AlarmDate;
            ContactId = reminderItem.ContactId;
            AlarmMessage = reminderItem.AlarmMessage;
            Status = reminderItem.Status;
            PreviousStatus = previousStatus;
        }

    }
}
