using Reminder.Storage.Core;
using Reminder.Storage.WebApi.Client;
using System;
using System.Collections.Generic;

namespace Test.WebApi.Client.App
{
	class Program
	{
		static void Main()
		{
			var client = new ReminderStorageWebApiClient("https://localhost:44344/");

			var reminderItem = new ReminderItemRestricted
			{
				ContactId = "TestContactId",
				Date = DateTimeOffset.Now,
				Message = "Test Message"
			};

			Console.Write("Clearing... ");
			client.Clear();
			WriteTestResult(client.Count == 0);
			
			Console.Write("Adding... ");
			Guid id = client.Add(reminderItem);
			WriteTestResult(true);

			Console.Write("Reading just added... ");
			var reminderItemTemp = client.Get(id);
			WriteTestResult(reminderItemTemp != null
			                && reminderItemTemp.Id == id);

			Console.Write("Reading all... ");
			var reminderItems = client.Get();
			WriteTestResult(reminderItems != null
			                 && reminderItems.Count == 1);
		
			Console.Write("Updating status to 'Awaiting'... ");
			client.UpdateStatus(id, ReminderItemStatus.Awaiting);
			reminderItemTemp = client.Get(id);
			WriteTestResult(reminderItemTemp != null
			                && reminderItemTemp.Status == ReminderItemStatus.Awaiting);

			Console.Write("Getting by status 'Awaiting'... ");
			reminderItems = client.Get(ReminderItemStatus.Awaiting);
			WriteTestResult(reminderItems != null
			                && reminderItems.Count == 1);

			Console.Write("Updating bulk status to 'Sent'... ");
			client.UpdateStatus(new List<Guid> { id }, ReminderItemStatus.Sent);
			reminderItemTemp = client.Get(id);
			WriteTestResult(reminderItemTemp != null
			                && reminderItemTemp.Status == ReminderItemStatus.Sent);

			Console.Write("Getting by status 'Sent'... ");
			reminderItems = client.Get(ReminderItemStatus.Sent);
			WriteTestResult(reminderItems != null
			                && reminderItems.Count == 1);

			Console.Write("Removing by Id and getting Count... ");
			client.Remove(id);
			WriteTestResult(client.Count == 0);
		}

		private static void WriteTestResult(bool ok)
		{
			if (ok)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("OK");
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Failed");
			}

			Console.ResetColor();
		}
	}
}
