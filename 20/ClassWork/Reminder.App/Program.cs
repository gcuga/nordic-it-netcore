using System;
using Reminder.Domain;
using Reminder.Domain.EventArgs;
using Reminder.Storage.Core;
using Reminder.Storage.InMemory;
using Reminder.Receiver.Core;
using Reminder.Sender.Core;
using Reminder.Receiver.Telegram;
using Reminder.Sender.Telegram;

// t.me/DNReminderBot.
// http api token: 957940944:AAFtAdUXu-uSW75lG87TpWw83ardWUbRiUg
// 2020-03-11T21:58:55 message

namespace Reminder.App
{
	class Program
	{
		static void Main(string[] args)
		{
			const string token = "957940944:AAFtAdUXu-uSW75lG87TpWw83ardWUbRiUg";
			IReminderReceiver reciever = new TelegramReminderReceiver(token);
			IReminderSender sender = new TelegramReminderSender(token);

			IReminderStorage storage = new InMemoryReminderStorage();
			ReminderDomain domain = new ReminderDomain(storage, reciever, sender);

			domain.ReminderItemStatusChanged += OnReminderItemStatusChanged;
			domain.ReminderItemSendingSucceeded += OnReminderItemSendingSucceeded;
			domain.ReminderItemSendingFailed += OnReminderItemSendingFailed;

			var item = new ReminderItem(
				Guid.NewGuid(),
				"TelegramContactId",
				DateTimeOffset.Now.AddSeconds(2),
				"Hello World ><");
			storage.Add(item);

			domain.Run();

			//List<ReminderItem> list = storage.Get(ReminderItemStatus.Awaiting);
			//foreach (var listItem in list)
			//{
			//	Console.WriteLine(listItem);
			//}

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void OnReminderItemStatusChanged(
			object sender,
			ReminderItemStatusChangedEventArgs e)
		{
			Console.WriteLine(
				$"Reminder of contact {e.Reminder.ContactId} " +
				$"has changed status from {e.Reminder.PreviousStatus} " +
				$"to {e.Reminder.Status} !!!");
		}

		private static void OnReminderItemSendingSucceeded(
			object sender,
			ReminderItemStatusChangedEventArgs e)
		{
			Console.WriteLine(
				$"Reminder of contact {e.Reminder.ContactId} " +
				$"sending succeeded and has changed status from {e.Reminder.PreviousStatus} " +
				$"to {e.Reminder.Status} !!!");
		}

		private static void OnReminderItemSendingFailed(
			object sender,
			ReminderItemSendingFailedEventArgs e)
		{
			Console.WriteLine(
				$"Reminder of contact {e.Reminder.ContactId} " +
				$"sending failed has changed status from {e.Reminder.PreviousStatus} " +
				$"to {e.Reminder.Status} !!!\n" +
				$"Error message {e.Reminder.SendingFailedException.Message}");
		}
	}
}
