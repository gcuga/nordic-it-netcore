using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Reminder.Storage.Core;

namespace Reminder.Domain.Tests
{
	public static class ReminderStorageMockProvider
	{
		public static Mock<IReminderStorage> GetReminderStorageMock(
			IDictionary<Guid, ReminderItem> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			}

			var storageMock = new Mock<IReminderStorage>();

			storageMock
				.Setup(x => x.Count)
				.Returns(() => dictionary.Count);

			storageMock
				.Setup(x => x.Add(It.IsAny<ReminderItemRestricted>()))
				.Returns<ReminderItemRestricted>(item =>
				{
					Guid id = Guid.NewGuid();
					dictionary.Add(id, 
						new ReminderItem
						{
							Id = id,
							ContactId = item.ContactId,
							Message = item.Message,
							Date = item.Date,
							Status = item.Status
						});

					return id;
				});

			storageMock
				.Setup(x => x.Remove(It.IsAny<Guid>()))
				.Returns<Guid>(dictionary.Remove);

			storageMock
				.Setup(x => x.Clear())
				.Callback(dictionary.Clear);

			storageMock
				.Setup(x => x.Get(It.IsAny<Guid>()))
				.Returns<Guid>(
					id => dictionary.ContainsKey(id) ? dictionary[id] : null);

			storageMock
				.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
				.Returns<int, int>((count, startPosition) =>
				{
					var reminders = dictionary.Values
						.Skip(startPosition);

					if (count != 0)
						reminders = reminders.Take(count);

					return reminders.ToList();
				});

			storageMock
				.Setup(x => x.Get(It.IsAny<ReminderItemStatus>(), 0, 0))
				.Returns<ReminderItemStatus, int, int>((status, count, startPosition) =>
				{
					var reminders = dictionary.Values
						.Where(x => x.Status == status)
						.Skip(startPosition);

					if (count != 0)
						reminders = reminders.Take(count);

					return reminders.ToList();
				});

			storageMock
				.Setup(x => x.UpdateStatus(It.IsAny<Guid>(), It.IsAny<ReminderItemStatus>()))
				.Callback<Guid, ReminderItemStatus>((id, status) =>
				{
					if (dictionary.ContainsKey(id))
						dictionary[id].Status = status;
				});

			storageMock
				.Setup(x => x.UpdateStatus(It.IsAny<IEnumerable<Guid>>(), It.IsAny<ReminderItemStatus>()))
				.Callback<IEnumerable<Guid>, ReminderItemStatus>((ids, status) =>
				{
					foreach (var id in dictionary.Keys.Where(ids.Contains))
						dictionary[id].Status = status;
				});

			return storageMock;
		}
	}
}
