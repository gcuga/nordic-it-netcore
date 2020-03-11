using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Reminder.Storage.Core.Tests
{
	[TestClass]
	public class ReminderItemTest
	{
		[TestMethod]
		public void Property_TimeToAlarm_Should_Be_Negative_For_Date_In_The_Past()
		{
			ReminderItem item = new ReminderItem(
				Guid.Empty,
				null,
				DateTimeOffset.Now.AddSeconds(-1),
				null);

			TimeSpan delta = item.TimeToAlarm;

			Assert.IsTrue(delta < TimeSpan.Zero);
		}
	}
}
