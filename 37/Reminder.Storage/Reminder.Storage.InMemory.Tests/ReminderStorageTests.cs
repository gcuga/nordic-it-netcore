using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Reminder.Storage.Core;

namespace Reminder.Storage.InMemory.Tests
{
	[TestClass]
	public class ReminderStorageTests
	{
		[TestMethod]
		public void Add_Method_Adds_Single_Reminder()
		{
			var reminder = new ReminderItemRestricted
			{
				Date = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(1)),
				Message = "Test",
				Status = ReminderItemStatus.Awaiting
			};

			var storage = new InMemoryReminderStorage();
			storage.Add(reminder);

			Assert.AreEqual(1, storage.Reminders.Count);
		}

		[TestMethod]
		public void Get_By_Id_Method_Returns_Null_For_Empty_Storage()
		{
			var storage = new InMemoryReminderStorage();

			var actual = storage.Get(Guid.Empty);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void Get_By_Id_Method_Returns_Single_Reminder()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(
				reminders.Select(id => storage.Add(id)));

			var expected = ids[2];
			var reminderItem = storage.Get(expected);

			Assert.IsNotNull(reminderItem);

			var actual = reminderItem.Id;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Remove_By_Id_Method_Returns_False_For_Empty_Storage()
		{
			var storage = new InMemoryReminderStorage();

			var actual = storage.Remove(Guid.Empty);

			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void Remove_By_Id_Method_Returns_True_When_Found_And_Removed()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			var itemIdToRemove = ids[2];

			var actual = storage.Remove(itemIdToRemove);

			Assert.IsTrue(actual);
			Assert.IsNull(storage.Get(itemIdToRemove));
		}

		[TestMethod]
		public void Get_Method_Without_Parameters_Returns_All_Reminders()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			var actual = storage.Get();

			Assert.IsNotNull(actual);
			Assert.AreEqual(4, actual.Count);
		}

		[TestMethod]
		public void Get_Method_With_Count_Only_Returns_Limited_Number_Of_Reminders()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			var actual = storage.Get(2);

			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count);
		}

		[TestMethod]
		public void Get_Method_With_Count_And_Start_Position_Returns_Limited_Number_Of_Reminders()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			var actual = storage.Get(2, 2);

			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count);
		}


		[TestMethod]
		public void Get_Method_With_Status_Only_Returns_All_Reminders_With_Requested_Status()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			List<ReminderItem> actual;

			actual = storage.Get(ReminderItemStatus.Awaiting);
			Assert.IsNotNull(actual);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Ready);
			Assert.IsNotNull(actual);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Sent);
			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed);
			Assert.IsNotNull(actual);
			Assert.AreEqual(0, actual.Count);
		}

		[TestMethod]
		public void UpdateStatus_Method_Updates_Single_Item_Correctly()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			storage.UpdateStatus(ids[0], ReminderItemStatus.Failed);

			var actual = storage.Get(ReminderItemStatus.Failed);
			Assert.IsNotNull(actual);
			Assert.AreEqual(1, actual.Count);
		}

		[TestMethod]
		public void UpdateStatus_Method_Updates_Many_Items_Correctly()
		{
			var reminders = GetReminderItemRestrictedList();
			var ids = new List<Guid>(reminders.Count);
			var storage = new InMemoryReminderStorage();
			ids.AddRange(reminders.Select(id => storage.Add(id)));

			storage.UpdateStatus(
				new[] { ids[1], ids[2] },
				ReminderItemStatus.Failed);

			var actual = storage.Get(ReminderItemStatus.Failed);
			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count);
		}

		private static List<ReminderItemRestricted> GetReminderItemRestrictedList()
		{
			return new List<ReminderItemRestricted>
			{
				new ReminderItemRestricted
				{
					Date = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(1)),
					Message = "Actual 1",
					Status = ReminderItemStatus.Awaiting
				},
				new ReminderItemRestricted
				{
					Date = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(-1)),
					Message = "Outdated 1",
					Status = ReminderItemStatus.Ready
				},
				new ReminderItemRestricted
				{
					Date = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(2)),
					Message = "Actual 2",
					Status = ReminderItemStatus.Sent
				},
				new ReminderItemRestricted
				{
					Date = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(-1)),
					Message = "Outdated 2",
					Status = ReminderItemStatus.Sent
				}
			};
		}
	}
}