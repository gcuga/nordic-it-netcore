using System;
using System.ComponentModel.DataAnnotations;
using Reminder.Storage.Core;

namespace Reminder.Storage.WebApi.Core
{
	public class ReminderItemCreateModel
	{
		/// <summary>
		/// Gets or sets the date and time the reminder item scheduled for sending.
		/// </summary>
		[Required]
		public DateTimeOffset Date { get; set; }

		/// <summary>
		/// Gets or sets contact identifier in the target sending system.
		/// </summary>
		[Required]
		public string ContactId { get; set; }

		/// <summary>
		/// Gets or sets the message of the reminder item for sending to the recipient.
		/// </summary>
		[Required]
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the status of the recipient.
		/// </summary>
		[Required]
		public ReminderItemStatus Status { get; set; }

		public ReminderItemCreateModel()
		{
		}

		public ReminderItemCreateModel(ReminderItemRestricted reminderItem)
		{
			Date = reminderItem.Date;
			ContactId = reminderItem.ContactId;
			Message = reminderItem.Message;
			Status = reminderItem.Status;
		}

		public ReminderItemRestricted ToReminderItemRestricted()
		{
			return new ReminderItemRestricted
			{
				ContactId = ContactId,
				Date = Date,
				Message = Message,
				Status = Status
			};
		}

		public ReminderItem ToReminderItem(Guid id)
		{
			return new ReminderItem
			{
				Id = id,
				ContactId = ContactId,
				Date = Date,
				Message = Message,
				Status = Status
			};
		}
	}
}
