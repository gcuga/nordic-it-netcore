using System;
using System.Drawing;
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
		static void Main()
		{
			var builder = new HostBuilder()
				.ConfigureServices((hostContext, serviceCollection) =>
				{
					IConfiguration config = new ConfigurationBuilder()
						.AddJsonFile(
							"appsettings.json",
							false,
							true)
						.Build();

					bool useProxy = bool.Parse(config["telegramBot.UseProxy"]);
					/*
					if (useProxy)
					{
						string telegramBotProxyHost = config["telegramBot.Proxy.Host"];
						int telegramBotProxyPort = int.Parse(config["telegramBot.Proxy.Port"]);

						serviceCollection.AddSingleton<IWebProxy>(new HttpToSocks5Proxy(
							telegramBotProxyHost,
							telegramBotProxyPort));
					}
					else
					{
						serviceCollection.AddSingleton((IWebProxy)null);
					}
					*/
					serviceCollection.AddHttpClient();
					serviceCollection.AddSingleton(config);
					serviceCollection.AddSingleton<IReminderStorage, ReminderStorageWebApiClient>();
					serviceCollection.AddSingleton<IReminderReceiver, TelegramReminderReceiver>();
					serviceCollection.AddSingleton<IReminderSender, TelegramReminderSender>();
					serviceCollection.AddSingleton<ReminderDomain, ReminderDomain>();
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

				ConsoleWrite(ConsoleColor.Green, "Reminder application is running...");
				Console.WriteLine("\nPress [Enter] to shutdown.");

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
			AddingSucceededEventArgs e)
		{
			Console.Write($"Reminder from contact ID {e.Reminder.ContactId} ");
			Console.Write($"with the message \"{e.Reminder.Message}\" ");
			ConsoleWrite(ConsoleColor.Green, "successfully scheduled");
			Console.WriteLine($" on {e.Reminder.Date:s}");
		}

		private static void Domain_SendingSucceeded(
			object sender,
			SendingSucceededEventArgs e)
		{
			Console.WriteLine($"Reminder {e.Reminder.Id:N} ");
			ConsoleWrite(ConsoleColor.Green, "successfully sent");
			Console.WriteLine($" message text \"{e.Reminder.Message}\"");
		}

		private static void Domain_SendingFailed(object sender, SendingFailedEventArgs e)
		{
			Console.Write($"Reminder {e.Reminder.Id:N} sending has failed. ");
			ConsoleWrite(ConsoleColor.Red, $"Exception:\n{e.Exception}");
		}

		public static void ConsoleWrite(ConsoleColor color, string text, params object[] arguments)
		{
			Console.ForegroundColor = color;
			Console.Write(text, arguments);
			Console.ResetColor();
		}
	}
}
