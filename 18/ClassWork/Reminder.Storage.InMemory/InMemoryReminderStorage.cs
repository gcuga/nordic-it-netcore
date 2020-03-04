using System;
using System.Collections.Generic;
using Reminder.Storage.Core;
using System.Linq;

namespace Reminder.Storage.InMemory
{
    public class InMemoryReminderStorage : IReminderStorage
    {
        internal readonly Dictionary<Guid, ReminderItem> ReminderItems = new Dictionary<Guid, ReminderItem>();

        public void Add(ReminderItem reminderItem)
        {
            ReminderItems.Add(reminderItem.Id, reminderItem);
        }

        public ReminderItem Get(Guid id)
        {
            ReminderItems.TryGetValue(id, out ReminderItem result);
            return result;
        }

        public List<ReminderItem> Get(ReminderItemStatus status)
        {
            return ReminderItems
                .Values
                .Where((x) => x.Status == status)
                .ToList();
         }

        public void Update(ReminderItem reminderItem)
        {
            ReminderItems[reminderItem.Id] = reminderItem;
        }
    }
}
