using System;
using Reminder.Storage.Core;

namespace Reminder.Storage.SqlServer.EF.Context
{
    public class ReminderItemDto
    {
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets contact identifier in the target sending system.
		/// </summary>
		public string ContactId { get; set; }

		/// <summary>
		/// Gets or sets the date and time the reminder item scheduled for sending.
		/// </summary>
		public DateTimeOffset TargetDate { get; set; }

		/// <summary>
		/// Gets or sets the message of the reminder item for sending to the recipient.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the identifier of the recipient.
		/// </summary>
		public ReminderItemStatus Status { get; set; }

		public DateTimeOffset CreatedDate { get; }

		public DateTimeOffset UpdatedDate  { get; }

		public ReminderItemDto()
		{
		}

		public ReminderItemDto(ReminderItemRestricted restricted)
		{
			ContactId = restricted.ContactId;
			TargetDate = restricted.Date;
			Message = restricted.Message;
			Status = restricted.Status;
		}

		public ReminderItem toReminderItem()
		{
			return new ReminderItem
			{
				Id = Id,
				ContactId = ContactId,
				Date = TargetDate,
				Message = Message,
				Status = Status
			};
		}
	}
}