﻿using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Repository.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext (DbContextOptions<OrderContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithOne(o => o.Customer).HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
           .HasMany(o => o.OrderItems)
           .WithOne(o => o.Order)
           .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Invoice>().HasOne(o => o.Order).WithOne(i => i.Invoice).HasForeignKey<Invoice>(i => i.OrderId);
            

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
               new Customer { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com" },
               new Customer { CustomerId = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
           );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, OrderDate = DateTime.Now, TotalAmount = 100, PaymentMethod = "Credit Card", Status = "Processing" },
                new Order { OrderId = 2, CustomerId = 2, OrderDate = DateTime.Now, TotalAmount = 200, PaymentMethod = "PayPal", Status = "Shipped" }
            );
        }


  


        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
