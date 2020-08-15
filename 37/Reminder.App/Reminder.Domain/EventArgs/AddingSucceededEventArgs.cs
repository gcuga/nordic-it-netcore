using System;
using Reminder.Domain.Model;

namespace Reminder.Domain.EventArgs
{
	public class AddingSucceededEventArgs: System.EventArgs
	{
		public AddReminderModel Reminder { get; set; }

		public Guid Id { get; set; }

		public AddingSucceededEventArgs(AddReminderModel reminder, Guid id)
		{
			Reminder = reminder;
			Id = id;
		}
	}
}
