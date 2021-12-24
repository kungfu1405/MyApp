using Core.Entity.SelfLearning;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebMvc.Models;

namespace WebMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EProduct>().ToTable("tblProduct");
            base.OnModelCreating(builder);
        }
        public DbSet<WebUser> WebUsers { get; set; }
        
        public DbSet<EProduct> Products { get; set; }
    }
}
