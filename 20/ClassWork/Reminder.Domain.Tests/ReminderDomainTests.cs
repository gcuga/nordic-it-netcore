using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Domain.EventArgs;
using Reminder.Storage.Core;
using Reminder.Storage.InMemory;
using System;

namespace Reminder.Domain.Tests
{
    [TestClass]
    public class ReminderDomainTests
    {
        [TestMethod]
        public void Check_That_On_SendReminder_OK_SendingSuccedded_Event_Raised()
        {
			// prepare test data
			var storage = new InMemoryReminderStorage();
			var source = new ReminderItem(
				Guid.NewGuid(),
				"TelegramContactId",
				DateTimeOffset.Now,
				"Hello World ><");
			source.Status = ReminderItemStatus.ReadyToSend;
			storage.Add(source);

			ReminderDomain domain = new ReminderDomain(storage);
			domain.ReminderItemSendingSucceeded += (
				object sender,
				ReminderItemStatusChangedEventArgs actual) =>
			{
				Assert.AreEqual(ReminderItemStatus.SuccessfullySent, source.Status);
			};

			// do the test
			domain.Run();


			// check the results


		}


		[TestMethod]
		public void Check_That_On_SendReminder_Exception_SendingFailed_Event_Raised()
		{
			// prepare test data
			// do the test
			// check the results
		}

		[TestMethod]
		public void Check_That_Reminder_Calls_Internal_Delegate()
		{
			// prepare test data
			// do the test
			// check the results
		}
	}
}
