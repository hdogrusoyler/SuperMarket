using Project.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMarket.Project.Entity
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class Product : IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int StockAmount { get; set; }
        public int Price { get; set; }
    }

    public class Category : IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class Cart : IEntity
    {
        public int Id { get; set; }
        public bool isPaymentCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }

    public class CartProduct : IEntity
    {
        public int Id { get; set; }
        public int ProductAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }

    public class SalesInformation : IEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int TotalPrice { get; set; }
    }
    public class PaymentType : IEntity
    {
        public int Id { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
