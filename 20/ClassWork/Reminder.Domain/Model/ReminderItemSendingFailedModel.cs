using System;
using Reminder.Storage.Core;


namespace Reminder.Domain.Model
{
	public class ReminderItemSendingFailedModel
	{
		public string ContactId { get; set; }

		public DateTimeOffset Date { get; set; }

		public string Message { get; set; }

		public ReminderItemStatus Status { get; set; }

		public ReminderItemStatus PreviousStatus { get; set; }

		public Exception SendingFailedException { get; set; }

		public ReminderItemSendingFailedModel()
		{

		}

		public ReminderItemSendingFailedModel(
			ReminderItem reminderItem,
			ReminderItemStatus previousStatus,
			Exception sendingFailedException)
		{
			Date = reminderItem.Date;
			ContactId = reminderItem.ContactId;
			Message = reminderItem.Message;
			Status = reminderItem.Status;
			PreviousStatus = previousStatus;
			SendingFailedException = sendingFailedException;
		}
	}
}
