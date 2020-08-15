using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reminder.Receiver.Core;
using Reminder.Sender.Core;
using Reminder.Storage.Core;

namespace Reminder.Domain.Tests
{
	[TestClass]
	public class ReminderDomainTests
	{
		[TestMethod]
		public void When_SendReminder_OK_SendingSucceeded_Event_Raised()
		{
			var item = new ReminderItem { Date = DateTimeOffset.Now };
			var dictionary = new Dictionary<Guid, ReminderItem>
			{
				{ item.Id, item}
			};

			var storageMock = ReminderStorageMockProvider.GetReminderStorageMock(dictionary);
			var receiverMock = new Mock<IReminderReceiver>();
			var senderMock = new Mock<IReminderSender>();

			using var reminderDomain = new ReminderDomain(
				storageMock.Object,
				receiverMock.Object,
				senderMock.Object,
				TimeSpan.FromMilliseconds(100),
				TimeSpan.FromMilliseconds(100));

			bool eventHandlerCalled = false;
			reminderDomain.SendingSucceeded += (s, e) => { eventHandlerCalled = true; };

			reminderDomain.Run();

			Thread.Sleep(300);

			Assert.IsTrue(eventHandlerCalled);
		}


		[TestMethod]
		public void When_SendReminder_Failed_SendingFailed_Event_Raised()
		{
			var item = new ReminderItem { Date = DateTimeOffset.Now };
			var dictionary = new Dictionary<Guid, ReminderItem>
			{
				{ item.Id, item}
			};

			var storageMock = ReminderStorageMockProvider.GetReminderStorageMock(dictionary);
			var receiverMock = new Mock<IReminderReceiver>();

			var senderMock = new Mock<IReminderSender>();
			senderMock
				.Setup(x => x.Send(It.IsAny<string>(), It.IsAny<string>()))
				.Throws(new Exception("Sending Failed Exception"));

			using var reminderDomain = new ReminderDomain(
				storageMock.Object,
				receiverMock.Object,
				senderMock.Object,
				TimeSpan.FromMilliseconds(100),
				TimeSpan.FromMilliseconds(100));

			bool eventHandlerCalled = false;
			reminderDomain.SendingFailed += (s, e) => { eventHandlerCalled = true; };

			reminderDomain.Run();

			Thread.Sleep(300);

			Assert.IsTrue(eventHandlerCalled);
		}

		[TestMethod]
		public void When_Receiver_Receives_New_Valid_Reminder_Event_AddingSucceeded_Raised()
		{
			var dictionary = new Dictionary<Guid, ReminderItem>();

			var storageMock = ReminderStorageMockProvider.GetReminderStorageMock(dictionary);
			var receiverMock = new Mock<IReminderReceiver>();
			var senderMock = new Mock<IReminderSender>();

			const string contact = "contact";
			const string message = "message";
			const string date = "2200-01-01T00:00:00Z";

			using var reminderDomain = new ReminderDomain(
				storageMock.Object,
				receiverMock.Object,
				senderMock.Object,
				TimeSpan.FromMilliseconds(100),
				TimeSpan.FromMilliseconds(100));

			bool eventHandlerCalled = false;
			reminderDomain.AddingSucceeded += (s, e) =>
			{
				Assert.AreEqual(contact, e.Reminder.ContactId);
				Assert.AreEqual(message, e.Reminder.Message);
				Assert.AreEqual(DateTimeOffset.Parse(date), e.Reminder.Date);
				Assert.AreNotSame(Guid.Empty, e.Id);
				eventHandlerCalled = true;
			};

			reminderDomain.Run();

			receiverMock.Raise(
				x => x.MessageReceived += null,
				new MessageReceivedEventArgs(contact, $"{date} {message}"));

			Thread.Sleep(300);

			Assert.IsTrue(eventHandlerCalled);
		}
	}
}
