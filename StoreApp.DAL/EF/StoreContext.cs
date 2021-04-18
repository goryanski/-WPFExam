using Microsoft.EntityFrameworkCore;
using StoreApp.DAL.Entities;
using StoreApp.DAL.Entities.ExtraTables;
using StoreApp.DAL.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.DAL.EF
{
    public class StoreContext : DbContext
    {
        private string connectionString = string.Empty;

        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<WroteOffProduct> WroteOffProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WarehouseSection> WarehouseSections { get; set; }
        public DbSet<Provisioner> Provisioners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }


        public StoreContext(string connectionString)
        {
            this.connectionString = connectionString;
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

