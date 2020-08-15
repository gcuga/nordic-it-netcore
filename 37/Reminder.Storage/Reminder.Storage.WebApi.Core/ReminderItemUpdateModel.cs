using System.ComponentModel.DataAnnotations;
using Reminder.Storage.Core;

namespace Reminder.Storage.WebApi.Core
{
	public class ReminderItemUpdateModel
	{
		/// <summary>
		/// Gets or sets the target status for the item.
		/// </summary>
		[Required]
		public ReminderItemStatus Status { get; set; }
	}
}
