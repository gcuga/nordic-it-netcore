using System;

namespace Reminder.Parsing
{
	public class ParsedMessage
	{
		public DateTimeOffset Date { get; set; }

		public string Message { get; set; }
	}
}