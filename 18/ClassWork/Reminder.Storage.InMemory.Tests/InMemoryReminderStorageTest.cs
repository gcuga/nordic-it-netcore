using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.InMemory;
using Reminder.Storage.Core;
using System;

namespace Reminder.Storage.InMemory.Tests
{
    [TestClass]
    public class InMemoryReminderStorageTest
    {
        [TestMethod]
        public void Method_Add_With_Not_Null_Item_Should_The_Item_Internally()
        {
            // prepare test data
            var storage = new InMemoryReminderStorage();
            var expected = new ReminderItem(Guid.NewGuid(), DateTimeOffset.Now, "Alarm");

            // do the test
            storage.Add(expected);

            // check the result
            var actualitem = storage.Get(expected.Id);
            Assert.IsNotNull(actualitem);
            Assert.AreEqual(expected.Id, actualitem.Id);
            Assert.AreEqual(expected.ContactId, actualitem.ContactId);
            Assert.AreEqual(expected.Status, actualitem.Status);
            Assert.AreEqual(expected.AlarmDate, actualitem.AlarmDate);
            Assert.AreEqual(expected.AlarmMessage, actualitem.AlarmMessage);
        }

        [TestMethod]
        public void Method_Get_By_Id_Should_Return_Null_For_Empty_Storage()
        {
            // prepare test data
            var storage = new InMemoryReminderStorage();

            // do the test
            var actual = storage.Get(Guid.Empty);

            // check the result
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Method_Get_By_Id_Should_Return_Not_Null_For_Existing_Item_In_Storage()
        {
            // prepare test data
            var storage = new InMemoryReminderStorage();
            var expected = new ReminderItem(Guid.NewGuid(), DateTimeOffset.Now, "Alarm");
            storage.ReminderItems.Add(expected.Id, expected);

            // do the test
            var actual = storage.Get(expected.Id);

            // check the result
            Assert.IsNotNull(actual);
        }
    }
}
