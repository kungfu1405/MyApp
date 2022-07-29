using DbData.Entitties;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal
{
   public class DbDataContext : IdentityDbContext
    {
        public DbDataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        //    builder.Entity<ECity>()
        //        .HasAlternateKey(c => c.Id)
        //.HasName("FK_City_Country_CountryId");
            //.HasAlternateKey(c => c.Id);
        }
        public DbSet<ECity> Cities { get; set; }
        //public DbSet<ECountry> Countries { get; set; }
        //public DbSet<EState> States { get; set; }
    }
}
