using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reminder.Storage.Core;

namespace Reminder.Storage.WebApi.Core
{
	public class ReminderItemsUpdateModel
	{
		/// <summary>
		/// Gets or sets the list of item ids to be updated.
		/// </summary>
		[Required]
		public List<Guid> Ids { get; set; }

		/// <summary>
		/// Gets or sets the target status for the item.
		/// </summary>
		[Required]
		public ReminderItemStatus Status { get; set; }
	}
}
