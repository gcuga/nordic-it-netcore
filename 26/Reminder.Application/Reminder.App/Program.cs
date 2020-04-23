using System;
using Reminder.Domain;
using Reminder.Domain.EventArgs;
using Reminder.Receiver.Telegram;
using Reminder.Sender.Telegram;
using Reminder.Storage.WebApi.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Reminder.App
{
	class Program
	{
		static void Main(string[] args)
		{
			IConfiguration config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false, true)
				.Build();


			var storage = new ReminderStorageWebApiClient(config["storageWebApiUrl"]);

			#region TestCode
			/* 
			*	var reminderItemRestricted = new ReminderItemRestricted
			*	{
			*		ContactId = "0909990909",
			*		Date = DateTimeOffset.Now,
			*		Message = "Test message",
			*		Status = ReminderItemStatus.Awaiting
			*	};
			*
			*	Guid id = storage.Add(reminderItemRestricted);
			*	ReminderItem reminderItem = storage.Get(id);
			*
			*	Console.WriteLine(
			*		JsonConvert.SerializeObject(reminderItem));
			*
			*	storage.UpdateStatus(id, ReminderItemStatus.Sent);
			*	reminderItem = storage.Get(id);
			*	Console.WriteLine(
			*		JsonConvert.SerializeObject(reminderItem));
			*/
			#endregion


			string telegramBotAccessToken = config["telegramBot.ApiToken"];
			//const string telegramBotProxyHost = "proxy.golyakov.net";
			//const int telegramBotProxyPort = 1080;

			//IWebProxy telegramProxy =
			//	new HttpToSocks5Proxy(string.Empty, telegramBotProxyPort);

			var receiver = new TelegramReminderReceiver(telegramBotAccessToken);
			var sender = new TelegramReminderSender(telegramBotAccessToken);

			var domain = new ReminderDomain(storage, receiver, sender);

			domain.AddingSucceeded += Domain_AddingSucceeded;
			domain.SendingSucceeded += Domain_SendingSucceeded;
			domain.SendingFailed += Domain_SendingFailed;

			domain.Run();

			Console.WriteLine(
				"Reminder application is running...\n" +
				"Press [Enter] to shutdown.");
			Console.ReadLine();
		}

		private static void Domain_AddingSucceeded(
			object sender,
			AddingSuccededEventArgs e)
		{
			Console.WriteLine(
				$"Reminder from contact ID {e.Reminder.ContactId} " +
				$"with the message \"{e.Reminder.Message}\" " +
				$"successfully scheduled on {e.Reminder.Date:s}");
		}

		private static void Domain_SendingSucceeded(
			object sender,
			SendingSuccededEventArgs e)
		{
			Console.WriteLine(
				"Reminder {0:N} successfully sent message text \"{1}\"",
				e.Reminder.Id,
				e.Reminder.Message);
		}

		private static void Domain_SendingFailed(object sender, SendingFailedEventArgs e)
		{
			Console.WriteLine(
				"Reminder {0:N} sending has failed. Exception:\n{1}",
				e.Reminder.Id,
				e.Exception);
		}

	}
}
