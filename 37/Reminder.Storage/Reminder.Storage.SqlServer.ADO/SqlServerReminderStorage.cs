using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Reminder.Storage.Core;

namespace Reminder.Storage.SqlServer.ADO
{
	public class SqlServerReminderStorage : IReminderStorage
	{
		private readonly string _connectionString;

		public SqlServerReminderStorage(string connectionString)
		{
			_connectionString = connectionString;
		}

		public SqlServerReminderStorage(IConfiguration config)
			: this(config.GetConnectionString("DefaultConnection"))
		{
		}

		public int Count
		{
			get
			{
				using var sqlConnection = GetOpenedSqlConnection();
				var cmd = sqlConnection.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "dbo.GetReminderItemsCount";
				return (int)cmd.ExecuteScalar();
			}
		}

		public Guid Add(ReminderItemRestricted reminder)
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.AddReminderItem";

			cmd.Parameters.AddWithValue("@contactId", reminder.ContactId);
			cmd.Parameters.AddWithValue("@targetDate", reminder.Date);
			cmd.Parameters.AddWithValue("@message", reminder.Message);
			cmd.Parameters.AddWithValue("@statusId", (byte)reminder.Status);

			var outputIdParameter = new SqlParameter("@reminderId", SqlDbType.UniqueIdentifier, 1);
			outputIdParameter.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(outputIdParameter);

			cmd.ExecuteNonQuery();

			return (Guid)outputIdParameter.Value;
		}

		public void Clear()
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.RemoveAllReminderItems";
			cmd.ExecuteNonQuery();
		}

		public ReminderItem Get(Guid id)
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.GetReminderItemById";

			cmd.Parameters.AddWithValue("@reminderId", id);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows || !reader.Read())
				return null;

			int idColumnIndex = reader.GetOrdinal("Id");
			int contactIdColumnIndex = reader.GetOrdinal("ContactId");
			int dateColumnIndex = reader.GetOrdinal("TargetDate");
			int messageColumnIndex = reader.GetOrdinal("Message");
			int statusIdColumnIndex = reader.GetOrdinal("StatusId");

			var result = new ReminderItem();
			result.Id = reader.GetGuid(idColumnIndex);
			result.ContactId = reader.GetString(contactIdColumnIndex);
			result.Date = reader.GetDateTimeOffset(dateColumnIndex);
			result.Message = reader.GetString(messageColumnIndex);
			result.Status = (ReminderItemStatus)reader.GetByte(statusIdColumnIndex);

			return result;
		}

		public List<ReminderItem> Get(int count = 0, int startPosition = 0)
		{
			var result = new List<ReminderItem>();

			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.GetReminderItemsWithPaging";

			cmd.Parameters.AddWithValue("@startPosition", startPosition);
			if (count > 0)
				cmd.Parameters.AddWithValue("@count", count);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows)
				return result;

			int idColumnIndex = reader.GetOrdinal("Id");
			int contactIdColumnIndex = reader.GetOrdinal("ContactId");
			int dateColumnIndex = reader.GetOrdinal("TargetDate");
			int messageColumnIndex = reader.GetOrdinal("Message");
			int statusIdColumnIndex = reader.GetOrdinal("StatusId");

			while (reader.Read())
			{
				var reminderItem = new ReminderItem();
				reminderItem.Id = reader.GetGuid(idColumnIndex);
				reminderItem.ContactId = reader.GetString(contactIdColumnIndex);
				reminderItem.Date = reader.GetDateTimeOffset(dateColumnIndex);
				reminderItem.Message = reader.GetString(messageColumnIndex);
				reminderItem.Status = (ReminderItemStatus)reader.GetByte(statusIdColumnIndex);
				result.Add(reminderItem);
			}
			return result;
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPosition = 0)
		{
			var result = new List<ReminderItem>();

			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.GetReminderItemsByStatusWithPaging";

			cmd.Parameters.AddWithValue("@statusId", (byte)status);
			cmd.Parameters.AddWithValue("@startPosition", startPosition);
			if (count > 0)
				cmd.Parameters.AddWithValue("@count", count);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows)
				return result;

			int idColumnIndex = reader.GetOrdinal("Id");
			int contactIdColumnIndex = reader.GetOrdinal("ContactId");
			int dateColumnIndex = reader.GetOrdinal("TargetDate");
			int messageColumnIndex = reader.GetOrdinal("Message");
			int statusIdColumnIndex = reader.GetOrdinal("StatusId");

			while (reader.Read())
			{
				var reminderItem = new ReminderItem();
				reminderItem.Id = reader.GetGuid(idColumnIndex);
				reminderItem.ContactId = reader.GetString(contactIdColumnIndex);
				reminderItem.Date = reader.GetDateTimeOffset(dateColumnIndex);
				reminderItem.Message = reader.GetString(messageColumnIndex);
				reminderItem.Status = (ReminderItemStatus)reader.GetByte(statusIdColumnIndex);
				result.Add(reminderItem);
			}
			return result;
		}

		public bool Remove(Guid id)
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.RemoveReminderItem";

			cmd.Parameters.AddWithValue("@reminderId", id);

			var outputIdParameter = new SqlParameter("@deleted", SqlDbType.Bit, 1);
			outputIdParameter.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(outputIdParameter);

			cmd.ExecuteNonQuery();

			return (bool)outputIdParameter.Value;
		}

		public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.UpdateReminderItem";

			cmd.Parameters.AddWithValue("@reminderId", Guid.Empty);
			cmd.Parameters.AddWithValue("@statusId", (byte)status);

			foreach (Guid id in ids)
			{
				cmd.Parameters["@reminderId"].Value = id;
				cmd.ExecuteNonQuery();
			}
		}

		public void UpdateStatus(Guid id, ReminderItemStatus status)
		{
			using var sqlConnection = GetOpenedSqlConnection();
			var cmd = sqlConnection.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "dbo.UpdateReminderItem";

			cmd.Parameters.AddWithValue("@reminderId", id);
			cmd.Parameters.AddWithValue("@statusId", (byte)status);

			cmd.ExecuteNonQuery();
		}

		private SqlConnection GetOpenedSqlConnection()
		{
			var sqlConnection = new SqlConnection(_connectionString);
			sqlConnection.Open();

			return sqlConnection;
		}
	}
}
