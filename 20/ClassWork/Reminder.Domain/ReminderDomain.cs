using System;
using System.Linq;
using System.Threading;
using Reminder.Domain.EventArgs;
using Reminder.Domain.Model;
using Reminder.Storage.Core;
using Reminder.Receiver.Core;
using Reminder.Sender.Core;
using Reminder.Parsing;

namespace Reminder.Domain
{
	public class ReminderDomain
	{
		private IReminderReceiver _receiver;
		private IReminderStorage _storage;
		private IReminderSender _sender;

		private Timer _awaitingRemindersCheckTimer;
		private Timer _readyReminderSendTimer;


		public event EventHandler<ReminderItemStatusChangedEventArgs> ReminderItemStatusChanged;
		public event EventHandler<ReminderItemStatusChangedEventArgs> ReminderItemSendingSucceeded;
		public event EventHandler<ReminderItemSendingFailedEventArgs> ReminderItemSendingFailed;

		public ReminderDomain(
			IReminderStorage storage,
			IReminderReceiver receiver,
			IReminderSender sender)
		{
			_storage = storage;
			_receiver = receiver;
			_sender = sender;
			_receiver.MessageReceived += ReceiverOnMessageReceived;
		}

		public void Run()
		{
			_awaitingRemindersCheckTimer = new Timer(
				CheckAwaitingReminders,
				null,
				TimeSpan.Zero,
				TimeSpan.FromSeconds(1));

			_readyReminderSendTimer = new Timer(
				SendReadyReminders,
				null,
				TimeSpan.Zero,
				TimeSpan.FromSeconds(1.5));

			_receiver.Run();
		}

		private void ReceiverOnMessageReceived(object sender, MessageReceivedEventArgs e)
		{
			// parsing of the e.Message to get
			var parsedMessage = MessageParser.ParseMessage(e.Message);
			if (parsedMessage == null)
			{
				// we can raise some MessageParsingFailed event
				Console.WriteLine($"Parsing {e.Message} has failed");
				return;
			}

			string alarmMessage = parsedMessage.Message;
			DateTimeOffset alarmDate = parsedMessage.Date;

			// adding new reminder item to the storage
			var item = new ReminderItem(
				Guid.NewGuid(),
				e.ContactId,
				alarmDate,
				alarmMessage);

			_storage.Add(item);

			// send message that reminder item was added

		}

		private void CheckAwaitingReminders(object dummy)
		{
			// read items in status Awaiting
			// check and if IsTimeToSend
			var items = _storage
				.Get(ReminderItemStatus.Awaiting)
				.Where(x => x.IsTimeToSend);

			// then update status to ReadyToSend
			foreach (var item in items)
			{
				var previousStatus = item.Status;
				item.Status = ReminderItemStatus.ReadyToSend;
				ReminderItemStatusChanged?.Invoke(
					this,
					new ReminderItemStatusChangedEventArgs(
						new ReminderItemStatusChangedModel(
							item,
							previousStatus)));
			}
		}

		private void SendReadyReminders(object dummy)
		{
			// read items in status ReadyToSend
			var items = _storage.Get(ReminderItemStatus.ReadyToSend);

			foreach (var item in items)
			{
				var previousStatus = item.Status;
				try
				{
					// and try to send
					_sender.Send(item.ContactId, item.Message);

					item.Status = ReminderItemStatus.SuccessfullySent;

					// if okay raise event SendingSucceeded
					ReminderItemSendingSucceeded?.Invoke(
						this,
						new ReminderItemStatusChangedEventArgs(
							new ReminderItemStatusChangedModel(
								item,
								previousStatus)));
				}
				catch (Exception sendingFailedException)
				{
					// if exception raise event SendingFailed
					item.Status = ReminderItemStatus.Failed;
					ReminderItemSendingFailed?.Invoke(
						this,
						new ReminderItemSendingFailedEventArgs (
							new ReminderItemSendingFailedModel(
								item,
								previousStatus,
								sendingFailedException
								)));

					// we don't want to rethrow original exception as
					// we already notified about it via the event
					// throw;
				}
			}
		}
	}
}
