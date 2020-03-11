using System;
using System.Collections.Generic;
using System.Linq;
using Reminder.Storage.Core;

namespace Reminder.Storage.InMemory
{
	

	public class InMemoryReminderStorage: IReminderStorage
	{
		internal readonly Dictionary<Guid, ReminderItem> Storage;

		public InMemoryReminderStorage()
		{
			Storage = new Dictionary<Guid, ReminderItem>();
		}

		public void Add(ReminderItem reminderItem)
		{
			Storage.Add(reminderItem.Id, reminderItem);
		}

		public void Update(ReminderItem reminderItem)
		{
			Storage[reminderItem.Id] = reminderItem;
		}

		public ReminderItem Get(Guid id)
		{
			return Storage.ContainsKey(id)
				? Storage[id]
				: null;
		}

		public List<ReminderItem> Get(ReminderItemStatus status)
		{
			//var result = new List<ReminderItem>();
			//foreach (var reminderItem in Storage.Values)
			//{
			//	if(reminderItem.Status == status)
			//		result.Add(reminderItem);
			//}
			//return result;

			return Storage
				.Values
				.Where(x => x.Status == status)
				.ToList();
		}
	}
}
