using System;
using System.Collections.Generic;
using Reminder.Storage.Core;
using System.Linq;

namespace Reminder.Storage.InMemory
{
    public class InMemoryReminderStorage : IReminderStorage
    {
        public EventHandler RunWhenUpdatingRun { get; set; }
        public EventHandler OnAddSuccess { get; set; }

        internal readonly Dictionary<Guid, ReminderItem> ReminderItems = new Dictionary<Guid, ReminderItem>();

        public void Add(ReminderItem reminderItem)
        {
            ReminderItems.Add(reminderItem.Id, reminderItem);
            if (OnAddSuccess != null)
            {
                OnAddSuccess(this, EventArgs.Empty);
            }
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
            if (RunWhenUpdatingRun != null)
            {
                RunWhenUpdatingRun(this, EventArgs.Empty);
            }
        }
    }
}
