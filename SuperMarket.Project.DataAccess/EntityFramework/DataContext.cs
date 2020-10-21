using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperMarket.Project.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuperMarket.Project.DataAccess.EntityFramework
{
    public class DataContext : DbContext
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options) : base(options) //, IHttpContextAccessor httpContextAccessor
        {
            //_httpContextAccessor = httpContextAccessor;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        public override Int32 SaveChanges()
        {
            var list = OnBeforeSaveChanges();
            var result = base.SaveChanges(acceptAllChangesOnSuccess: true);
            OnAfterSaveChanges(list);
            return result;
        }

        private List<SalesInformation> OnBeforeSaveChanges()
        {
            //var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            List<SalesInformation> list = new List<SalesInformation>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                    continue;
                if (entry.Entity is SalesInformation ent && entry.State == EntityState.Added)
                {
                    list.Add((SalesInformation)entry.Entity);
                    //foreach (var item in ent.Cart.CartProducts)
                    //{
                    //    Product pro = new Product();
                    //    pro.Id = item.Product.Id;
                    //    pro = Products.Find(pro);
                    //    pro.StockAmount -= item.ProductAmount;
                    //    Products.Update(pro);
                    //}
                }
            }
            return list;
        }

        private void OnAfterSaveChanges(List<SalesInformation> list)
        {
            //var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            if (list != null && list.Count > 0)
            {
                foreach (var ent in list)
                {
                    foreach (var item in ent.Cart.CartProducts)
                    {
                        Product pro = new Product();
                        pro = Products.Find(item.Product.Id);
                        pro.StockAmount -= item.ProductAmount;
                        Products.Update(pro);
                    }
                }
                SaveChanges();
            }
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
