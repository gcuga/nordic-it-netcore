using System;
using Reminder.Storage.Core;

namespace Reminder.Storage.WebApi.Core
{
	public class ReminderItemUpdateModel
	{
		/// <summary>
		/// Gets or sets the identifier of the recipient.
		/// </summary>
		public ReminderItemStatus Status { get; set; }
	}
}
