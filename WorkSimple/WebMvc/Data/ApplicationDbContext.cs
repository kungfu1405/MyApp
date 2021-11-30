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
      
        public DbSet<WebUser> WebUsers { get; set; }
        public DbSet<WebMvc.Models.Movie> Movie { get; set; }
        public DbSet<WebMvc.Data.EProduct> Productss { get; set; }
    }
}
