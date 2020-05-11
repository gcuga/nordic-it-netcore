using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MihaZupan;
using Reminder.Domain;
using Reminder.Domain.EventArgs;
using Reminder.Receiver.Core;
using Reminder.Receiver.Telegram;
using Reminder.Sender.Core;
using Reminder.Sender.Telegram;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Client;

namespace Reminder.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					IConfiguration config = new ConfigurationBuilder()
						.AddJsonFile(
							"appsettings.json",
							false,
							true)
						.Build();

					bool useProxy = bool.Parse(config["telegramBot.UseProxy"]);
					if (useProxy)
					{
						string telegramBotProxyHost = config["telegramBot.Proxy.Host"];
						int telegramBotProxyPort = int.Parse(config["telegramBot.Proxy.Port"]);

						services.AddSingleton<IWebProxy>(new HttpToSocks5Proxy(
							telegramBotProxyHost,
							telegramBotProxyPort,
							0));
					}
					else
					{
						services.AddSingleton((IWebProxy) null);
					}

					services.AddHttpClient();
					services.AddSingleton(config);
					services.AddSingleton<IReminderStorage, ReminderStorageWebApiClient>();
					services.AddSingleton<IReminderReceiver, TelegramReminderReceiver>();
					services.AddSingleton<IReminderSender, TelegramReminderSender>();
					services.AddSingleton<ReminderDomain, ReminderDomain>();
				}).UseConsoleLifetime();

			var host = builder.Build();

			using var serviceScope = host.Services.CreateScope();
			var services = serviceScope.ServiceProvider;

			try
			{
				var domain = services.GetRequiredService<ReminderDomain>();

				domain.AddingSucceeded += Domain_AddingSucceeded;
				domain.SendingSucceeded += Domain_SendingSucceeded;
				domain.SendingFailed += Domain_SendingFailed;

				domain.Run();

				Console.WriteLine(
					"Reminder application is running...\n" +
					"Press [Enter] to shutdown.");

				Console.ReadLine();
			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An error occurred.");
			}

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
