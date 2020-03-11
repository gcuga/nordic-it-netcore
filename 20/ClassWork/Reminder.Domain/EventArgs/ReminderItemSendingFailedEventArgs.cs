using Reminder.Domain.Model;

namespace Reminder.Domain.EventArgs
{
    public class ReminderItemSendingFailedEventArgs
    {
		public ReminderItemSendingFailedModel Reminder { get; set; }

		public ReminderItemSendingFailedEventArgs(ReminderItemSendingFailedModel reminder)
		{
			Reminder = reminder;
		}
	}
}
