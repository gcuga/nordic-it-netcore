using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reminder.Storage.Core;
using Reminder.Storage.SqlServer.ADO.Tests.Properties;
using Reminder.Storage.SqlServer.ADO;

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
			RunSqlScript(Resources.reminder);
			RunSqlScript(Resources.reminderSP);
		}

		[TestMethod]
		public void Add_Should_Return_Non_Empty_Guid()
		{
			IReminderStorage storage = new SqlServerReminderStorage(_connectionString);
			storage.Add(new ReminderItemRestricted
			{

			});
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
