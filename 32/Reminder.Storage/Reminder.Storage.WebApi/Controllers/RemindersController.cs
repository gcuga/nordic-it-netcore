using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Core;

namespace Reminder.Storage.WebApi.Controllers
{
	[ApiController]
	[Route("api/reminders")]
	public class RemindersController : ControllerBase
	{
		private readonly ILogger<RemindersController> _logger;
		private readonly IReminderStorage _reminderStorage;

		public RemindersController(
			ILogger<RemindersController> logger,
			IReminderStorage reminderStorage)
		{
			_logger = logger;
			_reminderStorage = reminderStorage;
		}

		[HttpGet]
		public IActionResult GetReminders(
			[FromQuery(Name = "status")] ReminderItemStatus status = (ReminderItemStatus)(-1),
			[FromQuery(Name = "count")] int count = -1,
			[FromQuery(Name = "startPosition")] int startPosition = 0)
		{
			List<ReminderItem> reminderItems =
				Enum.IsDefined(typeof(ReminderItemStatus), status)
					? _reminderStorage.Get(status, count, startPosition)
					: _reminderStorage.Get(count, startPosition);
			
			List<ReminderItemGetModel> reminderItemGetModels =
				reminderItems
					.Select(x => new ReminderItemGetModel(x))
					.ToList();

			return Ok(reminderItemGetModels);
		}

		[HttpGet("{id}")]
		public IActionResult GetReminder(Guid id)
		{
			var reminderItem = _reminderStorage.Get(id);
			if (reminderItem == null)
			{
				return NotFound();
			}

			return Ok(new ReminderItemGetModel(reminderItem));
		}


		[HttpPost]
		public IActionResult CreateReminder([FromBody] ReminderItemCreateModel reminderItemCreateModel)
		{
			if (reminderItemCreateModel == null)
			{
				return BadRequest();
			}

			var reminderItemRestricted = reminderItemCreateModel.ToReminderItemRestricted();
			Guid id = _reminderStorage.Add(reminderItemRestricted);

			var reminderItemGetModel = new ReminderItemGetModel(id, reminderItemRestricted);

			return CreatedAtAction(
				"GetReminder",
				new { id },
				reminderItemGetModel);
		}

		[HttpPatch("{id}")]
		public IActionResult UpdateReminder(Guid id, [FromBody] ReminderItemUpdateModel reminderItemUpdateModel)
		{
			var reminderItem = _reminderStorage.Get(id);
			if (reminderItem == null)
			{
				return NotFound();
			}

			_reminderStorage.UpdateStatus(id, reminderItemUpdateModel.Status);
			
			return NoContent();
		}
	}
}