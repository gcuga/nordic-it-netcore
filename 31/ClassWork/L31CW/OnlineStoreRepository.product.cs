using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace L31CW
{
    public partial class OnlineStoreRepository : IProductRepository
    {
        public int GetProductCount()
        {
            using var connection = GetOpenedSqlConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT COUNT(*) FROM [dbo].[Product]";
            return (int)command.ExecuteScalar();
        }

        public List<Tuple<int, string>> GetProductList()
        {
            using var connection = GetOpenedSqlConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT [Id], [Name] FROM [dbo].[Product]";
            using var reader = command.ExecuteReader();
            var result = new List<Tuple<int, string>>();
            if (!reader.HasRows)
            {
                return result;
            }

            int ordinalOfId = reader.GetOrdinal("Id");
            int ordinalOfName = reader.GetOrdinal("Name");

            while (reader.Read())
            {
                int id = reader.GetInt32(ordinalOfId);
                string name = reader.GetString(ordinalOfName);
                var record = new Tuple<int, string>(id, name);
                result.Add(record);
            }

            return result;
        }

        public int AddProduct(string name, decimal price)
        {
            using var connection = GetOpenedSqlConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "dbo.AddProduct";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@price", price);
            var idParameter = new SqlParameter("@id", SqlDbType.Int, 1);
            idParameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(idParameter);
            command.ExecuteNonQuery();
            return (int)idParameter.Value;
        }
    }
}
