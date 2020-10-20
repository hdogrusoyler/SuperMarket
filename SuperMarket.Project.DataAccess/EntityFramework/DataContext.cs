using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SuperMarket.Project.DataAccess.EntityFramework
{
    public class DataContext : DbContext
    {
        //public DataContext(DbContextOptions<DataContext> options) : base(options)
        //{ }
        public DataContext()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    //.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("CinemaDbContext"));

            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MarketDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=LAPTOP-TAPMU7Q5\\SQLEXPRESS;Database=MarketDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=EC2AMAZ-QA714VO\\SQLEXPRESS;Database=MarketDb;Trusted_Connection=True;MultipleActiveResultSets=true;Max Pool Size=100;Connect Timeout=3600;");
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<SalesInformation> SalesInformations { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
    }
}
