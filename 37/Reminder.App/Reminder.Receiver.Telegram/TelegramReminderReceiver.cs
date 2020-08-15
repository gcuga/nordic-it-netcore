using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Reminder.Receiver.Core;

namespace Reminder.Receiver.Telegram
{
	public class TelegramReminderReceiver : IReminderReceiver
	{
		private readonly TelegramBotClient _botClient;

		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		public TelegramReminderReceiver(string accessToken, IWebProxy proxy = null)
		{
			_botClient = proxy == null
				? new TelegramBotClient(accessToken)
				: new TelegramBotClient(accessToken, proxy);
		}

		public TelegramReminderReceiver(IConfiguration config, IWebProxy proxy = null)
		{
			string accessToken = config["telegramBot.ApiToken"];
			_botClient = proxy == null
				? new TelegramBotClient(accessToken)
				: new TelegramBotClient(accessToken, proxy);
		}

		public string GetHelloFromBot()
		{
			global::Telegram.Bot.Types.User user =
				_botClient.GetMeAsync().GetAwaiter().GetResult();

			return $"Hello from user {user.Id}." +
				$"My name is {user.FirstName} {user.LastName}";
		}

		public void Run()
		{
			_botClient.OnMessage += BotClient_OnMessage;
			_botClient.StartReceiving();
		}

		private void BotClient_OnMessage(
			object sender,
			global::Telegram.Bot.Args.MessageEventArgs e)
		{
			if (e.Message.Type == global::Telegram.Bot.Types.Enums.MessageType.Text)
			{
				OnMessageReceived(
					this,
					new MessageReceivedEventArgs(
						e.Message.Chat.Id.ToString(),
						e.Message.Text));
			}
		}

		protected virtual void OnMessageReceived(object sender, MessageReceivedEventArgs e)
		{
			MessageReceived?.Invoke(sender, e);
		}
	}
}
