using System;
using System.Data;
using System.Data.SqlClient;
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
	public class SqlServerReminderStorageTest
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
			RunSqlScript(Resources.DatabaseSchema);
			RunSqlScript(Resources.DatabaseSPs);
			RunSqlScript(Resources.DatabaseTestData);
		}

		[TestMethod]
		public void Method_Add_Should_Return_Non_Empty_Guid_If_All_Correct()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			
			Guid actual = storage.Add(new ReminderItemRestricted
			{
				ContactId = "ContactId",
				Date = DateTimeOffset.Now,
				Message = "Message",
				Status = ReminderItemStatus.Awaiting
			});

			Assert.AreNotEqual(Guid.Empty, actual);
		}

		[TestMethod]
		public void Method_Get_By_ReminderItemId_Should_Return_ReminderItem_If_Exists()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);

			Guid id = Guid.Parse("00000000-0000-0000-0000-111111111111");
			ReminderItem actual = storage.Get(id);
			
			Assert.IsNotNull(actual);
		}


		[TestMethod]
		public void Method_Get_By_ReminderItemId_Should_Return_Null_If_Does_Not_Exist()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);

			Guid id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
			ReminderItem actual = storage.Get(id);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void Method_Get_With_Paging_Should_Return_All_Records_With_Default_Parameters()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var actual = storage.Get();
			Assert.IsNotNull(actual);
			Assert.AreEqual(8, actual.Count);
		}

		[TestMethod]
		public void Method_Get_With_Paging_Should_Return_3_Records_From_2_to_4_For_Parameters_3_2()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var actual = storage.Get(3, 2);
		
			Assert.IsNotNull(actual);
			Assert.AreEqual(3, actual.Count);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-333333333333"), actual[0].Id);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-444444444444"), actual[1].Id);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-555555555555"), actual[2].Id);
		}

		[TestMethod]
		public void Method_Get_By_Sent_Status_With_Default_Paging_Should_Return_2_Records_With_4_And_5()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var actual = storage.Get(ReminderItemStatus.Sent);
			Assert.IsNotNull(actual);
			Assert.AreEqual(2, actual.Count);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-444444444444"), actual[0].Id);
			Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-555555555555"), actual[1].Id);
		}

		[DataTestMethod]
		[DataRow(ReminderItemStatus.Failed, 1, 1, "00000000-0000-0000-0000-777777777777")]
		[DataRow(ReminderItemStatus.Sent, 1, 0, "00000000-0000-0000-0000-444444444444")]
		public void Method_Get_By_Some_Status_With_Paging_Should_Return_1_Record_With_Corresponded_Id(
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
		public void Method_Update_By_Id_Should_Update_Status_To_Given(string guid, ReminderItemStatus status)
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
			foreach (string sqlInstruction in sqlInstructions)
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
