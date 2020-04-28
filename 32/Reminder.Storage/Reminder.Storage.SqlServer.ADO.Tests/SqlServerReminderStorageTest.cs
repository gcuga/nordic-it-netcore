using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.Core;
using Reminder.Storage.SqlServer.ADO.Tests.Properties;

namespace Reminder.Storage.SqlServer.ADO.Tests
{
	[TestClass]
	[DoNotParallelize]
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
			RunSqlScript(Resources.DatabaseVWs);
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
		public void Method_Get_Should_Return_ReminderItem_For_Existing_Guid()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			DateTimeOffset expectedNow = DateTimeOffset.Now;
			command.CommandText =
				@"INSERT INTO [dbo].[ReminderItem] ([Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate])"
				+ @" VALUES ('25768B31-C59C-4BBA-B63A-38584E826809','usr',@now,'Msg',0,SYSDATETIMEOFFSET(),SYSDATETIMEOFFSET())";
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = expectedNow;
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var expectedId = new Guid("25768B31-C59C-4BBA-B63A-38584E826809");
			ReminderItem actual = storage.Get(expectedId);

			Assert.IsNotNull(actual);
			Assert.AreEqual(expectedId, actual.Id);
			Assert.AreEqual("usr", actual.ContactId);
			Assert.AreEqual(expectedNow, actual.Date);
			Assert.AreEqual("Msg", actual.Message);
			Assert.AreEqual(ReminderItemStatus.Awaiting, actual.Status);
		}

		[TestMethod]
		public void Method_Get_Should_Return_Null_For_Nonexistent_Guid()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			var id = new Guid("5AA6AD38-96A0-483C-9267-D9A6983DC47F");
			ReminderItem actual = storage.Get(id);

			Assert.IsNull(actual);
		}

		[TestMethod]
		public void Method_Get_Should_Return_Empty_List_For_Empty_ReminderItem_Table()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			List<ReminderItem> actual = storage.Get();

			Assert.AreEqual(0, actual.Count);
		}

		[TestMethod]
		public void Method_Get_Should_Return_All_Records_With_Default_Count_And_Start_Position()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 1',2)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 2',1)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 3',3)";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			List<ReminderItem> actual = storage.Get();

			Assert.AreEqual(3, actual.Count);
		}

		[TestMethod]
		public void Method_Get_Should_Return_All_Records_Starting_From_A_Given_Position()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 1',2)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 2',1)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', SYSDATETIMEOFFSET(),'Message 3',3)";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			List<ReminderItem> actual = storage.Get(startPosition: 1);

			Assert.AreEqual(2, actual.Count);
		}

		[TestMethod]
		public void Method_Get_Should_Return_Empty_List_For_Status_Nonexistent_In_Table()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			DateTimeOffset now = DateTimeOffset.Now;
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr1', @now,'Message 10',2)";
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now;
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr1', @now,'Message 20',0)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr1', @now,'Message 30',0)";
			command.ExecuteNonQuery();
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr1', @now,'Message 40',3)";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			List<ReminderItem> actual = storage.Get(ReminderItemStatus.Ready);

			Assert.AreEqual(0, actual.Count);
		}

		[TestMethod]
		public void Method_Get_Should_Return_Certain_Number_Of_Records_For_Given_Status_Count_Position()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			DateTimeOffset now = DateTimeOffset.Now;
			now = now.AddTicks( - (now.Ticks % TimeSpan.TicksPerSecond));
			// now
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now;
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 1',2)";
			command.ExecuteNonQuery();
			// now + 1 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(1);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 2',1)";
			command.ExecuteNonQuery();
			// now + 2 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(2);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 3',0)";
			command.ExecuteNonQuery();
			// now + 3 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(3);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 4',3)";
			command.ExecuteNonQuery();
			// now + 4 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(4);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 5',3)";
			command.ExecuteNonQuery();
			// now + 5 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(5);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 6',3)";
			command.ExecuteNonQuery();
			// now + 6 second
			command.Parameters.Clear();
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = now.AddSeconds(6);
			command.CommandText =
				@"INSERT INTO[dbo].[VW_ReminderItem] ([ContactId],[TargetDate],[Message],[StatusId])"
				+ @" VALUES('usr', @now,'Message 7',0)";
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			List<ReminderItem> actual = storage.Get(ReminderItemStatus.Failed, 1, 1);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 2, 1);
			Assert.AreEqual(2, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 0, 2);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 3, 5);
			Assert.AreEqual(0, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 2, 0);
			Assert.AreEqual(2, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 1, 0);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 1, 1);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 1, 2);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Failed, 1, 3);
			Assert.AreEqual(0, actual.Count);

			actual = storage.Get(ReminderItemStatus.Awaiting, 0, 1);
			Assert.AreEqual(1, actual.Count);

			actual = storage.Get(ReminderItemStatus.Awaiting, 0, 0);
			Assert.AreEqual(2, actual.Count);
		}

		[TestMethod]
		public void Method_UpdateStatus_Should_Change_Status_For_Existing_Id()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			DateTimeOffset expectedNow = DateTimeOffset.Now;
			command.CommandText =
				@"INSERT INTO [dbo].[ReminderItem] ([Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate])"
				+ @" VALUES ('35DED3B6-7207-452E-B09C-75EA17F77693','usr',@now,'Msg',@status,SYSDATETIMEOFFSET(),SYSDATETIMEOFFSET())";
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = expectedNow;
			command.Parameters.Add("@status", SqlDbType.TinyInt).Value = (byte)ReminderItemStatus.Awaiting;
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			storage.UpdateStatus(new Guid("35DED3B6-7207-452E-B09C-75EA17F77693"), ReminderItemStatus.Sent);

			command.Parameters.Clear();
			command.CommandText =
				@"SELECT [StatusId] FROM [dbo].[ReminderItem]" +
				@" WHERE [Id]='35DED3B6-7207-452E-B09C-75EA17F77693'";
			var actualStatus = (ReminderItemStatus)((byte)command.ExecuteScalar());

			Assert.AreEqual(ReminderItemStatus.Sent, actualStatus);
		}

		[TestMethod]
		public void Method_UpdateStatus_Should_Not_Change_Status_For_Other_Id()
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"DELETE [dbo].[ReminderItem]";
			command.ExecuteNonQuery();
			DateTimeOffset expectedNow = DateTimeOffset.Now;
			command.CommandText =
				@"INSERT INTO [dbo].[ReminderItem] ([Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate])"
				+ @" VALUES ('35DED3B6-7207-452E-B09C-75EA17F77693','usr',@now,'Msg',@status,SYSDATETIMEOFFSET(),SYSDATETIMEOFFSET())";
			command.Parameters.Add("@now", SqlDbType.DateTimeOffset).Value = expectedNow;
			command.Parameters.Add("@status", SqlDbType.TinyInt).Value = (byte)ReminderItemStatus.Ready;
			command.ExecuteNonQuery();

			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			storage.UpdateStatus(new Guid("1CA09791-21E5-45B0-973A-86CDFBA7A2D3"), ReminderItemStatus.Sent);

			command.Parameters.Clear();
			command.CommandText =
				@"SELECT [StatusId] FROM [dbo].[ReminderItem]" +
				@" WHERE [Id]='35DED3B6-7207-452E-B09C-75EA17F77693'";
			var actualStatus = (ReminderItemStatus)((byte)command.ExecuteScalar());

			Assert.AreEqual(ReminderItemStatus.Ready, actualStatus);
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
