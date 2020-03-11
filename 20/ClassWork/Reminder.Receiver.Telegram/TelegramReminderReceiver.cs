using System;
using System.Net;
using Reminder.Receiver.Core;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Reminder.Receiver.Telegram
{
    public class TelegramReminderReceiver : IReminderReceiver
    {
        private TelegramBotClient _botClient;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void Run()
        {
            _botClient.OnMessage += BotClientOnMessage;
            _botClient.StartReceiving();
        }

        private void BotClientOnMessage(object sender, global::Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Text)
            {
                OnMessageReceived(
                    this,
                    new MessageReceivedEventArgs(
                        e.Message.Text,
                        e.Message.Chat.Id.ToString()));
            }
        }

        public TelegramReminderReceiver(string token, IWebProxy proxy = null)
        {
            if (proxy != null)
            {
                _botClient = new TelegramBotClient(token, proxy);
            }
            else
            {
                _botClient = new TelegramBotClient(token);
            }
        }

        protected virtual void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(sender, e);
        }
    }
}
