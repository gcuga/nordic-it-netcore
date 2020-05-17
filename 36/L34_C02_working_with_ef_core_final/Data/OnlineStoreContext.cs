using Microsoft.EntityFrameworkCore;
using L34_C02_working_with_ef_core_final.Domain;

namespace L34_C02_working_with_ef_core_final.Data
{
	public class OnlineStoreContext : DbContext
	{
		private readonly string _connectionString;

		public DbSet<Product> Products { get; set; }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Order> Orders { get; set; }

		public OnlineStoreContext()
		{
			_connectionString =
				@"Data Source=ASUSN56V\MSSQLD;" +
				"Initial Catalog=OnlineStoreEF;" +
				"Integrated Security=true;";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Customer>()
					.HasAlternateKey(p => p.Name)
					.HasName("UQ_Customers_Name");

			modelBuilder
				.Entity<OrderItem>()
					.HasKey("OrderId", "ProductId")
					.HasName("PK_OrderItems")
					.IsClustered(true);
		}
	}
}
