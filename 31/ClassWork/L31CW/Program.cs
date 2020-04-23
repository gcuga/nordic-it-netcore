using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace L31CW
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new OnlineStoreRepository(
                @"Data Source = ASUSN56V\MSSQLD; Initial Catalog=OnlineStore; Integrated Security = True;"
                + @"Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False;"
                + @"ApplicationIntent = ReadWrite; MultiSubnetFailover = False");

            // products
            //exampleOfProducts(repo);

            // orders
            exampleOfOrders(repo);

            var productIdCountList = new List<Tuple<int, int>>();
            productIdCountList.Add(new Tuple<int, int>(1, 2));
            productIdCountList.Add(new Tuple<int, int>(2, 1));
            productIdCountList.Add(new Tuple<int, int>(3, 10));
            productIdCountList.Add(new Tuple<int, int>(4, 5));
            productIdCountList.Add(new Tuple<int, int>(5, 3));

            repo.AddOrder(3, DateTimeOffset.Now, 0.05, productIdCountList);

            productIdCountList.Clear();
            productIdCountList.Add(new Tuple<int, int>(16, 3));
            productIdCountList.Add(new Tuple<int, int>(17, 2));
            productIdCountList.Add(new Tuple<int, int>(18, 1));

            repo.AddOrder(6, DateTimeOffset.Now, null, productIdCountList);

            exampleOfOrders(repo);
        }

        static void exampleOfOrders(OnlineStoreRepository repo)
        {
            var orderList = repo.GetOrderList();

            foreach (var record in orderList)
            {
                Console.WriteLine(
                    $"Order ID = {record.Item1}, Customer name = \"{record.Item2}\""
                    + $", Date = {record.Item3}"
                    + (record.Item4.HasValue ? $", Discount = {record.Item4 * 100} %" : string.Empty));
            }

            Console.WriteLine($"Total number of orders = {repo.GetOrderCount()}");
        }

        static void exampleOfProducts(OnlineStoreRepository repo)
        {
            int id = repo.AddProduct("Superphone", 99999.99M);
            Console.WriteLine($"Superphone with price {99999.99M} added, id = {id}");

            var records = repo.GetProductList();

            foreach (var record in records)
            {
                Console.WriteLine($"Product ID = {record.Item1}, Name = \"{record.Item2}\"");
            }

            Console.WriteLine($"Total number of products = {repo.GetProductCount()}");
        }

        static void example1()
        {
            string connectionString =
            @"Data Source = ASUSN56V\MSSQLD; Initial Catalog=OnlineStore; Integrated Security = True;"
            + @"Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False;"
            + @"ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandType = System.Data.CommandType.Text;

                sqlCommand.CommandText = @"select count(*) from [dbo].[Customer]";
                var numberOfCustomers = (int)sqlCommand.ExecuteScalar();
                Console.WriteLine($"numberOfCustomers = {numberOfCustomers}");

                sqlCommand.CommandText = @"
                        SELECT C.[Id], C.[Name]
                        FROM [dbo].[Customer] AS C
                        WHERE C.[Id] IN (
	                        SELECT O.CustomerId
	                        FROM [dbo].[Order] AS O
	                        WHERE YEAR(O.OrderDate) = 2018)";

                var firstCustomerId = (int)sqlCommand.ExecuteScalar();
                Console.WriteLine($"firstCustomerId = {firstCustomerId}");

                using var reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    int ordinalOfId = reader.GetOrdinal("Id");
                    int ordinalOfName = reader.GetOrdinal("Name");

                    while (reader.Read())
                    {
                        int customerId = reader.GetInt32(ordinalOfId);
                        string customerName = reader.GetString(ordinalOfName);
                        Console.WriteLine($"customerId = {customerId}, customerName = {customerName}");
                    }
                }
            }
        }
    }
}
