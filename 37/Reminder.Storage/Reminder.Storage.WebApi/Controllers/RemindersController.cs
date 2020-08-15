using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Core;
using Swashbuckle.AspNetCore.Annotations;

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
		[SwaggerOperation(
			Summary = "Gets the reminder items with ability to filter by status.",
			Description = "Can be called with paging settings: 'count' and 'startPosition'.",
			Tags = new[] { "Required" })]
		public IActionResult GetReminders(
			[FromQuery(Name = "status"),
			 SwaggerParameter("Status filter", Required = false)] 
			ReminderItemStatus status = (ReminderItemStatus)(- 1),
			[FromQuery(Name = "count"),
			 SwaggerParameter("Maximum number for the result, 0 for unlimited results.", Required = false)] 
			int count = 0,
			[FromQuery(Name = "startPosition"),
			 SwaggerParameter("Start position: zero-based index of the item to start from.", Required = false)]
			int startPosition = 0)
		{
			IEnumerable<ReminderItemGetModel> reminderItemGetModels;

			if (status < 0)
			{
				reminderItemGetModels = _reminderStorage
					.Get(count, startPosition)
					.Select(ri => new ReminderItemGetModel(ri));
			}
			else
			{
				reminderItemGetModels = _reminderStorage
					.Get(status, count, startPosition)
					.Select(ri => new ReminderItemGetModel(ri));
			}

			return Ok(reminderItemGetModels);
		}

		[HttpGet("{id}")]
		[SwaggerOperation(
			Summary = "Gets the reminder item by ID.",
			Description = "Not throws an exception if not found.",
			Tags = new[] { "Optional" })]
		[SwaggerResponse(200, "The reminder item was found.", typeof(ReminderItemGetModel))]
		[SwaggerResponse(404, "The reminder item with specified ID was not found.")]
		public IActionResult GetReminder(
			[SwaggerParameter("The identifier of the reminder item.", Required = true)]
			Guid id)
		{
			var reminderItem = _reminderStorage.Get(id);
			if (reminderItem == null)
			{
				return NotFound();
			}

			return Ok(new ReminderItemGetModel(reminderItem));
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Creates the new reminder item based on the restricted model.",
			Description = "Returns the full reminder item included created by storage ID.",
			Tags = new[] { "Required" })]
		[SwaggerResponse(201, "The reminder item was found", typeof(ReminderItemGetModel))]
		[SwaggerResponse(400, "The restricted reminder item model is invalid.")]
		public IActionResult CreateReminder(
			[FromBody, SwaggerParameter("Restricted reminder item model that does not contain ID.", Required = true)]
			ReminderItemCreateModel reminderItemCreateModel)
		{
			if (reminderItemCreateModel == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var reminderItemRestricted = reminderItemCreateModel.ToReminderItemRestricted();
			Guid id = _reminderStorage.Add(reminderItemRestricted);

			var reminderItemGetModel = new ReminderItemGetModel(
				reminderItemCreateModel.ToReminderItem(id));

			return CreatedAtAction(
				"GetReminder",
				new { id },
				reminderItemGetModel);
		}

		[HttpPatch("{id}")]
		[SwaggerOperation(
			Summary = "Updates the status of single reminder item with specified ID.",
			Description = "Returns 204 No Content if status successfully updated.",
			Tags = new[] { "Required" })]
		[SwaggerResponse(204, "The reminder item status successfully updated.")]
		[SwaggerResponse(400, "The model of update is invalid.")]
		[SwaggerResponse(404, "The reminder item with specified ID was not found.")]
		public IActionResult UpdateReminderStatus(
			[SwaggerParameter("The identifier of the reminder item.", Required = true)]
			Guid id,
			[FromBody]
			[SwaggerParameter("Reminder item update model, now contains only status.", Required = true)]
			ReminderItemUpdateModel reminderItemUpdateModel)
		{
			var reminderItem = _reminderStorage.Get(id);

			if (reminderItem == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_reminderStorage.UpdateStatus(id, reminderItemUpdateModel.Status);

			return NoContent();
		}

		[HttpPatch]
		[SwaggerOperation(
			Summary = "Updates the status of a set of reminder items with specified ID list.",
			Description = "Not found items skipped without throwing the exception. Returns 204 No Content.",
			Tags = new[] { "Required" })]
		[SwaggerResponse(204, "The reminder item status successfully updated.")]
		[SwaggerResponse(400, "The model of update is invalid.")]
		public IActionResult UpdateRemindersStatus(
			[FromBody]
			[SwaggerParameter("Reminder items update model, contains list of identifiers and status.", Required = true)]
			ReminderItemsUpdateModel reminderItemsUpdateModel)
		{
			if (reminderItemsUpdateModel == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_reminderStorage.UpdateStatus(
				reminderItemsUpdateModel.Ids,
				reminderItemsUpdateModel.Status);

			return NoContent();
		}


		[HttpHead]
		[SwaggerOperation(
			Summary = "Gets the number of the reminder items in the storage.",
			Description = "Returns the result in 'X-Total-Count' header.",
			Tags = new[] { "Optional" })]
		[SwaggerResponse(200, "Number of items successfully returned.")]
		public IActionResult GetRemindersCount()
		{
			Response.Headers.Add("X-Total-Count", _reminderStorage.Count.ToString());
			return Ok();
		}

		[HttpPut]
		[SwaggerOperation(
			Summary = "Clears storage of reminder items.",
			Description = "Returns 200 OK.",
			Tags = new[] { "Optional" })]
		[SwaggerResponse(204, "The storage successfully cleaned.")]
		public IActionResult ClearReminders()
		{
			_reminderStorage.Clear();
			return NoContent();
		}

		[HttpDelete("{id}")]
		[SwaggerOperation(
			Summary = "Deletes the reminder item with specified ID.",
			Description = "Returns 200 OK or 404 Not Found.",
			Tags = new[] { "Optional" })]
		[SwaggerResponse(204, "The reminder item was successfully deleted.")]
		[SwaggerResponse(404, "The reminder item with specified ID was not found.")]
		public IActionResult DeleteReminder(
			[SwaggerParameter("The identifier of the reminder item.", Required = true)]
			Guid id)
		{
			var reminderItem =
				_reminderStorage
					.Get(id);

			if (reminderItem == null)
			{
				return NotFound();
			}

			_reminderStorage.Remove(id);

			return NoContent();
		}
	}
}