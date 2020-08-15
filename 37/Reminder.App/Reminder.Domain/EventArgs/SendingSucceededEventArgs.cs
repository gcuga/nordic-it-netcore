using Reminder.Domain.Model;

namespace Reminder.Domain.EventArgs
{
	public class SendingSucceededEventArgs: System.EventArgs
	{
		public SendReminderModel Reminder { get; set; }

		public SendingSucceededEventArgs(SendReminderModel reminder)
		{
			Reminder = reminder;
		}
	}
}
