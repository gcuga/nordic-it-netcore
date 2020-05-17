using System;
using System.Collections.Generic;
using System.Linq;
using L34_C01_working_with_ef_core.Data;
using L34_C01_working_with_ef_core.Domain;

namespace L34_C01_working_with_ef_core
{
	class Person
	{
		public string FirstName { get; set; }
	}

	class Program
	{
		static void Main()
		{
			//InsertCustomers();

			//InsertProducts();

			//SelectCustomers();

			//SelectProducts();

			UpdateProducts();

		}

		private static void InsertCustomers()
		{
			var customer = new Customer { Name = "Masha"};

			using var context = new OnlineStoreContext();
			context.Add(customer);

			//var customer2 = context.Customers.FirstOrDefault(c => c.Name.StartsWith("Al"));

			//Console.WriteLine(customer2.Name);

			//context.SaveChanges();
		}

		private static void InsertProducts()
		{
			var product = new Product { Name = "Вертолет", Price = 100000.99M };

			using var context = new OnlineStoreContext();

			context.Add(product);
			context.SaveChanges();

			var products = new List<Product>
			{
				new Product { Name = "Forerunner 645 Music", Price = 42199.99M },
				new Product { Name = "MARQ Aviator", Price = 208400 }
			};

			context.AddRange(products);
			context.SaveChanges();
		}

		private static void SelectCustomers()
		{
			using var context = new OnlineStoreContext();

			var customers = context.Customers.ToList();

			customers.ForEach(x => Console.WriteLine(x.Name));
		}

		private static void SelectProducts()
		{
			using var context = new OnlineStoreContext();

			// связанный параметр в запросе
			string name = "Forerunner 645 Music";
			var product = context.Products.FirstOrDefault(p => p.Name == name);
			Console.WriteLine(product.Name);

			// литерал в запросе
			var product1 = (from p in context.Products where p.Name == "MARQ Aviator" select p).First();
			Console.WriteLine(product1.Name);
		}

		private static void UpdateProducts()
		{
			using var context = new OnlineStoreContext();

			var products = context.Products.ToList();

			products.ForEach(x => { x.Name += " со скидкой"; x.Price *= 0.9M; });

			context.UpdateRange(products);
			context.SaveChanges();
		}
	}
}
