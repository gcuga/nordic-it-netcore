using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace L31CW
{
    public partial class OnlineStoreRepository : IOrderRepository
    {
        const string _orderListQuery = @"
select [Id]
     , (select Name from [dbo].[Customer] c
        where c.[id] = o.[CustomerId]) as [CustomerName]
     , [OrderDate]
     , [Discount]
  from [dbo].[Order] o";

        public int GetOrderCount()
        {
            using var connection = GetOpenedSqlConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT COUNT(*) FROM [dbo].[Order]";
            return (int)command.ExecuteScalar();
        }

        public List<Tuple<int, string, DateTimeOffset, double?>> GetOrderList()
        {
            var result = new List<Tuple<int, string, DateTimeOffset, double?>>();
            using var connection = GetOpenedSqlConnection();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = _orderListQuery;
            using var reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                return result;
            }

            int ordinalOfId = reader.GetOrdinal("Id");
            int ordinalOfName = reader.GetOrdinal("CustomerName");
            int ordinalOfDate = reader.GetOrdinal("OrderDate");
            int ordinalOfDiscount = reader.GetOrdinal("Discount");

            while (reader.Read())
            {
                int id = reader.GetInt32(ordinalOfId);
                string name = reader.GetString(ordinalOfName);
                DateTimeOffset orderDate = reader.GetDateTimeOffset(ordinalOfDate);
                double? discount;
                if (reader.IsDBNull(ordinalOfDiscount))
                {
                    discount = null;
                }
                else
                {
                    discount = reader.GetDouble(ordinalOfDiscount);
                }

                var record = new Tuple<int, string, DateTimeOffset, double?>(id, name, orderDate, discount);
                result.Add(record);
            }

            return result;
        }

        public int AddOrder(
            int customerId,
            DateTimeOffset orderDate,
            double? discount,
            List<Tuple<int, int>> productIdCountList)
        {
            using var connection = GetOpenedSqlConnection();
            using SqlTransaction transaction = connection.BeginTransaction();
            using SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            try
            {
                // добавляем заказ
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"[dbo].[AddOrder]";
                command.Parameters.AddWithValue("@customerId", customerId);
                command.Parameters.AddWithValue("@orderDate", orderDate);
                if (discount == null)
                {
                    command.Parameters.Add("@discount", SqlDbType.Float).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.AddWithValue("@discount", (double)discount);
                }

                var orderIdOutput = new SqlParameter("@id", SqlDbType.Int, 1);
                orderIdOutput.Direction = ParameterDirection.Output;
                command.Parameters.Add(orderIdOutput);
                command.ExecuteNonQuery();
                if (orderIdOutput.Value == DBNull.Value || (int)orderIdOutput.Value < 1)
                {
                    throw new Exception("Ошибка при добавлении заказа");
                }

                var orderId = (int)orderIdOutput.Value;

                // добавляем позиции
                DataTable tvp = new DataTable();
                tvp.Columns.Add("orderId", typeof(int));
                tvp.Columns.Add("productId", typeof(int));
                tvp.Columns.Add("numberOfItems", typeof(int));
                foreach (var item in productIdCountList)
                {
                    tvp.Rows.Add(orderId, item.Item1, item.Item2);
                }

                command.Parameters.Clear();
                command.CommandText = @"[dbo].[AddOrderItem]";
                command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tvp",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "OrderItemTableType",
                        Value = tvp,
                    });
                command.ExecuteNonQuery();

                transaction.Commit();
                return orderId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
