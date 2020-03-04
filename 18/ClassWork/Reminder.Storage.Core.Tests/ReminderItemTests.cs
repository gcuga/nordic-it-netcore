using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Reminder.Storage.Core.Tests
{
    [TestClass]
    public class ReminderItemTests
    {
        [TestMethod]
        public void Property_Time_To_Alarm_Should_Be_Negative_For_Date_In_The_Past()
        {
            // prepare test data
            var item = new ReminderItem(Guid.Empty, DateTimeOffset.Now - TimeSpan.FromSeconds(10), string.Empty);

            // do the test
            var expected = item.TimeToAlarm;

            Assert.IsTrue(expected.Ticks < 0);
        }
    }
}
