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
            //  modelBuilder.Entity<Student>()
            //.HasOne<StudentAddress>(s => s.Address)
            //.WithOne(ad => ad.Student)
            //.HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);
            builder.Entity<EProduct>()
                  .HasOne<ECategory>(c => c.Category)
                  .WithMany(pro => pro.Products)
                  .HasForeignKey(c => c.CateId);

            base.OnModelCreating(builder);
        }
        public DbSet<WebUser> WebUsers { get; set; }
        
        public DbSet<EProduct> Products { get; set; }
        public DbSet<ECategory> Categories{ get; set; }

    }
}
