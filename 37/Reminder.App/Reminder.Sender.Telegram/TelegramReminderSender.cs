using System.Net;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Reminder.Sender.Core;

namespace Reminder.Sender.Telegram
{
	public class TelegramReminderSender : IReminderSender
	{
		private readonly TelegramBotClient _botClient;

		public TelegramReminderSender(string accessToken, IWebProxy proxy = null)
		{
			_botClient = proxy == null
				? new TelegramBotClient(accessToken)
				: new TelegramBotClient(accessToken, proxy);
		}

		public TelegramReminderSender(IConfiguration config, IWebProxy proxy = null)
		{
			string accessToken = config["telegramBot.ApiToken"];
			_botClient = proxy == null
				? new TelegramBotClient(accessToken)
				: new TelegramBotClient(accessToken, proxy);
		}

		public void Send(string contactId, string message)
		{
			var chatId =
				new global::Telegram.Bot.Types.ChatId(
					long.Parse(contactId));

			_botClient.SendTextMessageAsync(
				chatId,
				message);
		}
	}
}
