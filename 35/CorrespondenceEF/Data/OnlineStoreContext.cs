using System;
using System.Collections.Generic;
using System.Text;
using CorrespondenceEF.Domain;
using Microsoft.EntityFrameworkCore;


namespace CorrespondenceEF.Data
{
    class OnlineStoreContext : DbContext
    {
        private readonly string _connectionString =
            "Data Source=ASUSN56V\\MSSQLD;Initial Catalog=CorrespondenceEF;Integrated Security=true;";

        public DbSet<City> Cities { get; set; }

        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
