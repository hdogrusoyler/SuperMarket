using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using SuperMarket.Project.Entity;
using SuperMarket.Project.Entity.CustomModel;
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

        private EntityEntryList OnBeforeSaveChanges()
        {
            //var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            EntityEntryList list = new EntityEntryList();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                if (entry.Entity is CartProduct)
                {
                    if (entry.State == EntityState.Added)
                    {
                        list.AddedEntityState = true;
                        list.AddedCartProductList.Add((CartProduct)entry.Entity);
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        list.ModifiedEntityState = true;
                        list.ModifiedCartProductList.Add((CartProduct)entry.Entity);
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        list.DeletedEntityState = true;
                        list.DeletedCartProductList.Add((CartProduct)entry.Entity);
                    }
                }
            }
            return list;
        }

        private void OnAfterSaveChanges(EntityEntryList list)
        {
            //var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            Product pro = new Product();
            if (list.AddedCartProductList != null && list.AddedCartProductList.Count > 0 && list.AddedEntityState == true)
            {
                foreach (var item in list.AddedCartProductList)
                {
                    pro = Products.Find(item.ProductId);
                    pro.StockAmount -= 1;
                    Products.Update(pro);
                }
                SaveChanges();
            }
            else if (list.ModifiedCartProductList != null && list.ModifiedCartProductList.Count > 0 && list.ModifiedEntityState == true)
            {
                foreach (var item in list.ModifiedCartProductList)
                {
                    pro = Products.Find(item.ProductId);
                    pro.StockAmount -= 1;
                    Products.Update(pro);
                }
                SaveChanges();
            }
            else if (list.DeletedCartProductList != null && list.DeletedCartProductList.Count > 0 && list.DeletedEntityState == true)
            {
                foreach (var item in list.DeletedCartProductList)
                {
                    pro = Products.Find(item.ProductId);
                    pro.StockAmount += item.ProductAmount;
                    Products.Update(pro);
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
