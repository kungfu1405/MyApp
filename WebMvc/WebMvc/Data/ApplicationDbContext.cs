using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebMvc.Models;

namespace WebMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomerEntity> // DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductOrdersEntity>().HasKey(table => new {
                table.OrderID,
                table.ProductID
            });
            //builder.Entity<OrderEnity>().HasKey(table => new {
            //    table.CustomerID
            //});

            //builder.Entity<CustomerEntity>(eb => { eb.HasNoKey(); });
            builder.Entity<CustomerEntity>().HasMany<OrderEnity>(order => order.Orders);
         
        }
        //public DbSet<WebUser> WebUsers { get; set; }
        public DbSet<CategoryEntity> Categorys { get; set; }
        //public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<ProductOrdersEntity> ProductOrders { get; set; }
        public DbSet<OrderEnity> Orders { get; set; }
    }
}
