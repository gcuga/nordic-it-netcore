using L34_C01_working_with_ef_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace L34_C01_working_with_ef_core.Data
{
	public class OnlineStoreContext : DbContext
	{
		private readonly string _connectionString =
			@"Data Source=ASUSN56V\MSSQLD;" +
			"Initial Catalog=OnlineStoreEFcore;" +
			"Integrated Security=true;";


		public static readonly ILoggerFactory MyConsoleLoggerFactory =
					LoggerFactory.Create(builder => {
						builder.AddFilter("Microsoft", LogLevel.Warning)
							.AddFilter("System", LogLevel.Warning)
							.AddFilter("SampleApp.Program", LogLevel.Debug)
							.AddConsole();
					}
				);

		public DbSet<Product> Products { get; set; }

		public DbSet<Customer> Customers { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Order> Orders { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			optionsBuilder.UseLoggerFactory(MyConsoleLoggerFactory);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Product>()
				.ToTable("Product")
				.HasIndex(x => x.Price)
				.HasName("IX_Product_Price");

			modelBuilder
				.Entity<Product>()
				.HasKey(x => x.Id).HasName("PK_Product");

			modelBuilder
				.Entity<Product>()
				.Property(x => x.Name)
				.HasColumnType("VARCHAR(100)");

			//modelBuilder
			//	.Entity<Product>()
			//	.HasAlternateKey(x => x.Name).HasName("UQ_Product_Name");



			modelBuilder
				.Entity<OrderItem>()
				.HasKey(x => new {x.OrderId, x.ProductId});
		}
	}
}
