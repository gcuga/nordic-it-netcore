using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.Core;
using Reminder.Storage.SqlServer.ADO.Tests.Properties;

//[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
//[DoNotParallelize]
namespace Reminder.Storage.SqlServer.ADO.Tests
{
	[TestClass]
	public class SqlServerReminderStorageTests
	{
		private static string _connectionString;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			_connectionString = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build()
				.GetConnectionString("DefaultConnection");
		}

		[TestInitialize]
		public void TestInitialize()
		{
			RunSqlScript(Resources.Schema);
			RunSqlScript(Resources.SPs);
			RunSqlScript(Resources.Data);
		}

		[TestMethod]
		public void Add_Then_Get_By_Id_Methods_Returns_Just_Added_Item()
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			DateTimeOffset expectedDate = DateTimeOffset.Now;
			string expectedContactId = "TEST_CONTACT_ID";
			string expectedMessage = "TEST_MESSAGE_TEXT";
			ReminderItemStatus expectedStatus = ReminderItemStatus.Awaiting;

			Guid id = storage.Add(new ReminderItemRestricted
			{
				ContactId = expectedContactId,
				Date = expectedDate,
				Message = expectedMessage,
				Status = expectedStatus
			});

			Assert.AreNotEqual(Guid.Empty, id);

			var actualItem = storage.Get(id);

			Assert.IsNotNull(actualItem);
			Assert.AreEqual(id, actualItem.Id);
			Assert.AreEqual(expectedDate, actualItem.Date);
			Assert.AreEqual(expectedContactId, actualItem.ContactId);
			Assert.AreEqual(expectedMessage, actualItem.Message);
			Assert.AreEqual(expectedStatus, actualItem.Status);
		}

		[TestMethod]
		public void Get_By_Id_Method_Returns_Not_Null_Item_With_Proper_Fields()
		{
			Guid expectedGuid = Guid.Parse("00000000-0000-0000-0000-111111111111");
			DateTimeOffset expectedDate = DateTimeOffset.Parse("2020-01-01 00:00:00 +00:00");
			string expectedContactId = "ContactId_1";
			string expectedMessage = "Message_1";
			ReminderItemStatus expectedStatus = ReminderItemStatus.Awaiting;

			var storage = new SqlServerReminderStorage(_connectionString);
			var actualItem = storage.Get(expectedGuid);

			Assert.IsNotNull(actualItem);
			Assert.AreEqual(expectedGuid, actualItem.Id);
			Assert.AreEqual(expectedContactId, actualItem.ContactId);
			Assert.AreEqual(expectedDate, actualItem.Date);
			Assert.AreEqual(expectedMessage, actualItem.Message);
			Assert.AreEqual(expectedStatus, actualItem.Status);
		}

		[TestMethod]
		public void Get_By_Id_Method_Returns_Null_If_Not_Found()
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			var actual = storage.Get(Guid.Empty);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void Remove_By_Id_Method_Returns_False_For_Not_Found_Reminder()
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			var actual = storage.Remove(Guid.Empty);

			Assert.IsFalse(actual);
		}

		[TestMethod]
		public void Remove_By_Id_Method_Returns_True_When_Found_And_Removed()
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			Guid id = Guid.Parse("00000000-0000-0000-0000-111111111111");
			var actual = storage.Remove(id);

			Assert.IsTrue(actual);

			var removedItem = storage.Get(id);

			Assert.IsNull(removedItem);
		}

		[TestMethod]
		public void Get_Method_Without_Parameters_Returns_All_Reminders()
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			var actual = storage.Get();

			Assert.IsNotNull(actual);
			Assert.AreEqual(8, actual.Count);
		}

		[DataTestMethod]
		[DataRow(ReminderItemStatus.Awaiting, 2)]
		[DataRow(ReminderItemStatus.Ready, 1)]
		[DataRow(ReminderItemStatus.Sent, 2)]
		[DataRow(ReminderItemStatus.Failed, 3)]
		public void Get_Method_With_Status_Only_Returns_All_Reminders_With_Requested_Status(
			ReminderItemStatus status,
			int expectedCount)
		{
			var storage = new SqlServerReminderStorage(_connectionString);

			List<ReminderItem> actual = storage.Get(status);
			Assert.IsNotNull(actual);
			Assert.AreEqual(expectedCount, actual.Count);
		}

		[TestMethod]
		public void Get_With_Paging_Method_Should_Return_3_Records_From_2_to_4_For_Parameters_3_2()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var actual = storage.Get(3, 2);

			Assert.IsNotNull(actual);
			Assert.AreEqual(3, actual.Count);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-333333333333"), actual[0].Id);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-444444444444"), actual[1].Id);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-555555555555"), actual[2].Id);
		}

		[DataTestMethod]
		[DataRow(ReminderItemStatus.Failed, 1, 1, "00000000-0000-0000-0000-777777777777")]
		[DataRow(ReminderItemStatus.Sent, 1, 0, "00000000-0000-0000-0000-444444444444")]
		public void Get_By_Some_Status_With_Paging_Method_Should_Return_1_Record_With_Corresponded_Id(
			ReminderItemStatus status,
			int count,
			int startPosition,
			string guid)
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var actual = storage.Get(status, count, startPosition);

			Assert.IsNotNull(actual);
			Assert.AreEqual(1, actual.Count);
			Assert.AreEqual(Guid.Parse(guid), actual[0].Id);
		}

		[DataTestMethod]
		[DataRow("00000000-0000-0000-0000-777777777777", ReminderItemStatus.Sent)]
		public void Update_Status_By_Id_Method_Should_Update_Status_To_Given(string guid, ReminderItemStatus status)
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			storage.UpdateStatus(Guid.Parse(guid), status);
			var actual = storage.Get(Guid.Parse(guid));
			Assert.AreEqual(status, actual.Status);
		}

		private void RunSqlScript(string script)
		{
			using SqlConnection connection = GetOpenedSqlConnection();
			SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;

			string[] sqlInstructions = Regex.Split(script, @"\bGO\b");
			foreach (string sqlInstruction in sqlInstructions
				.Where(s => !string.IsNullOrWhiteSpace(s)))
			{
				command.CommandText = sqlInstruction;
				command.ExecuteNonQuery();
			}
		}

		private SqlConnection GetOpenedSqlConnection()
		{
			var sqlConnection = new SqlConnection(_connectionString);
			sqlConnection.Open();
			return sqlConnection;
		}

	}
}
