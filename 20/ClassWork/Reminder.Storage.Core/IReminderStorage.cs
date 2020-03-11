using System;
using System.Collections.Generic;
using System.Text;

namespace Reminder.Storage.Core
{
	public interface IReminderStorage
	{
		void Add(ReminderItem reminderItem);

		void Update(ReminderItem reminderItem);

		ReminderItem Get(Guid id);

		List<ReminderItem> Get(ReminderItemStatus status);
	}
}
