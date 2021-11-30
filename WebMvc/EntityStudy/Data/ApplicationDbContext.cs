using EntityStudy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Text;

namespace EntityStudy.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomerEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CustomerEntity>().ToTable("Customer");
            builder.Entity<IdentityRole>().ToTable("Role");
            //builder.Entity<IdentityUserRole>().ToTable("UserRoles");
            //builder.Entity<IdentityUserClaim>().ToTable("UserRole");
            builder.Entity<CategoryEntity>().HasMany<ProductEntity>()
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
     
    public DbSet<CategoryEntity> Categorys { get; set; }
    //public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }    
    public DbSet<ProductOrdersEntity> ProductOrders { get; set; }
    public DbSet<OrderEnity> Orders { get; set; }

}
}
