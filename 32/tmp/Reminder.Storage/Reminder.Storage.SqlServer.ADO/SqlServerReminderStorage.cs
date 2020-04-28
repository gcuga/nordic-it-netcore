using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Reminder.Storage.Core;
using System.Data;

namespace Reminder.Storage.SqlServer.ADO
{
    public class SqlServerReminderStorage : IReminderStorage
    {
        private string _connectionString;

        public SqlServerReminderStorage(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private SqlConnection GetOpenedSqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        public void UpdateStatus(IEnumerable<Guid> ids, ReminderItemStatus status)
        {
            foreach (var item in ids)
            {
                UpdateStatus(item, status);
            }
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



        #region NotImplemented
        public int Count => throw new NotImplementedException();


        #endregion




        public void Clear()
        {
            throw new NotImplementedException();
        }

        public ReminderItem Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ReminderItem> Get(int count = -1, int startPosition = 0)
        {
            throw new NotImplementedException();
        }

        public List<ReminderItem> Get(ReminderItemStatus status, int count = 0, int startPosition = 0)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(Guid id, ReminderItemStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
