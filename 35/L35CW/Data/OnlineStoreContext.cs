using System;
using System.Collections.Generic;
using System.Text;
using L35CW.Domain;
using Microsoft.EntityFrameworkCore;


namespace L35CW.Data
{
    class OnlineStoreContext : DbContext
    {
        private readonly string _connectionString = 
            "Data Source=ASUSN56V\\MSSQLD;Initial Catalog=OnlineStoreEF;Integrated Security=true;";

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
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
                .HasAlternateKey(x => x.Name).HasName("UQ_Product_Name");
            modelBuilder
                .Entity<Product>()
                .Property(x => x.Name)
                .HasColumnType("VARCHAR(100)");
        }
    }
}
