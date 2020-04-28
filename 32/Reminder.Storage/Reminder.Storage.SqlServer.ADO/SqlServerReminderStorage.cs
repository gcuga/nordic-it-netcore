using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Reminder.Storage.Core;

namespace Reminder.Storage.SqlServer.ADO
{
	public class SqlServerReminderStorage: IReminderStorage
	{
		private const string _nameOfId = "[Id]";
		private const string _nameOfContactId = "[ContactId]";
		private const string _nameOfTargetDate = "[TargetDate]";
		private const string _nameOfMessage = "[Message]";
		private const string _nameOfStatusId = "[StatusId]";

		private string _connectionString;

		public SqlServerReminderStorage(string connectionString)
		{
			_connectionString = connectionString;
		}

		public Guid Add(ReminderItemRestricted reminder)
		{
			using var connection = GetOpenedSqlConnection();
			var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "[dbo].[AddReminderItem]";
			command.Parameters.AddWithValue("@contactId", reminder.ContactId);
			command.Parameters.AddWithValue("@targetDate", reminder.Date);
			command.Parameters.AddWithValue("@message", reminder.Message);
			command.Parameters.AddWithValue("@status", (byte)reminder.Status);
			return (Guid)command.ExecuteScalar();
		}

		public ReminderItem Get(Guid id)
		{
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"SELECT " + 
				_nameOfId + "," +
				_nameOfContactId + "," +
				_nameOfTargetDate + "," +
				_nameOfMessage + "," +
				_nameOfStatusId +
				" FROM [dbo].[VW_ReminderItem] WHERE [Id] = @id";
			command.Parameters.AddWithValue("@id", SqlDbType.UniqueIdentifier).Value = id;
			using var reader = command.ExecuteReader();

			if (!reader.HasRows)
			{
				return null;
			}

			reader.Read();

			return new ReminderItem(
				reader.GetGuid(TrimSquareBrackets(_nameOfId)),
				reader.GetString(TrimSquareBrackets(_nameOfContactId)),
				reader.GetDateTimeOffset(reader.GetOrdinal(TrimSquareBrackets(_nameOfTargetDate))),
				reader.GetString(TrimSquareBrackets(_nameOfMessage)),
				(ReminderItemStatus)reader.GetByte(TrimSquareBrackets(_nameOfStatusId))
			);
		}

		private List<ReminderItem> Get(ReminderItemStatus? status, int count = 0, int startPosition = 0)
		{
			var result = new List<ReminderItem>();
			using var connection = GetOpenedSqlConnection();
			using SqlCommand command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"SELECT " +
				_nameOfId + "," +
				_nameOfContactId + "," +
				_nameOfTargetDate + "," +
				_nameOfMessage + "," +
				_nameOfStatusId +
				" FROM [dbo].[VW_ReminderItem]" +
				((status != null) ? (" WHERE " + _nameOfStatusId + "=@status") : string.Empty) +
				" ORDER BY [ContactId], [TargetDate], [UpdatedDate], [Id] OFFSET @offset ROWS" +
				((count > 0) ? " FETCH NEXT @count ROWS ONLY" : string.Empty);
			if (status != null)
			{
				command.Parameters.AddWithValue("@status", SqlDbType.TinyInt).Value = (byte)status;
			}
			command.Parameters.AddWithValue("@offset", SqlDbType.Int).Value = startPosition;
			command.Parameters.AddWithValue("@count", SqlDbType.Int).Value = count;
			using var reader = command.ExecuteReader();

			if (!reader.HasRows)
			{
				return result;
			}

			while (reader.Read())
			{
				result.Add(
					new ReminderItem(
						reader.GetGuid(TrimSquareBrackets(_nameOfId)),
						reader.GetString(TrimSquareBrackets(_nameOfContactId)),
						reader.GetDateTimeOffset(reader.GetOrdinal(TrimSquareBrackets(_nameOfTargetDate))),
						reader.GetString(TrimSquareBrackets(_nameOfMessage)),
						(ReminderItemStatus)reader.GetByte(TrimSquareBrackets(_nameOfStatusId))));
			}

			return result;
		}

		public List<ReminderItem> Get(int count = 0, int startPosition = 0)
		{
			return Get((ReminderItemStatus?)null, count, startPosition);
		}

		public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPosition = 0)
		{
			return Get((ReminderItemStatus?)status, count, startPosition);
		}

		public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
		{
			foreach (var id in ids)
			{
				UpdateStatus(id, status);
			}
		}

		public void UpdateStatus(Guid id, ReminderItemStatus status)
		{
			using var connection = GetOpenedSqlConnection();
			var command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"UPDATE [dbo].[VW_ReminderItem] SET [StatusId]=@statusId WHERE [Id]=@id";
			command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
			command.Parameters.Add("@statusId", SqlDbType.TinyInt).Value = (byte)status;
			command.ExecuteNonQuery();
		}

		private SqlConnection GetOpenedSqlConnection()
		{
			var sqlConnection = new SqlConnection(_connectionString);
			sqlConnection.Open();
			return sqlConnection;
		}

		private string TrimSquareBrackets(string s)
		{
			return s.Trim(new char[] { '[', ']' });
		}

		#region Temporarily out of scope

		public int Count => throw new NotImplementedException();

		public bool Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
