using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reminder.Storage.InMemory;
using Reminder.Receiver.Core;
using Reminder.Sender.Core;

namespace Reminder.Domain.Tests
{
	[TestClass]
	public class ReminderDomainTests
	{
		public Mock<IReminderReceiver> receiverMock = new Mock<IReminderReceiver>();
		public Mock<IReminderSender> senderMock = new Mock<IReminderSender>();

		[TestMethod]
		public void When_SendReminder_OK_SendingSuccedded_Event_Raised()
		{
			var reminderStorage = new InMemoryReminderStorage();
			using (var reminderDomain = new ReminderDomain(
				reminderStorage,
				receiverMock.Object,
				senderMock.Object,
				TimeSpan.FromMilliseconds(100),
				TimeSpan.FromMilliseconds(100)))
			{
				bool eventHandlerCalled = false;

				reminderDomain.SendingSucceeded += (s, e) =>
				{
					eventHandlerCalled = true;
				};

				reminderStorage.Add(new Storage.Core.ReminderItemRestricted
				{ 
					Date = DateTimeOffset.Now
				});

				reminderDomain.Run();

				Thread.Sleep(300);

				Assert.IsTrue(eventHandlerCalled);
			}
		}
	}
}