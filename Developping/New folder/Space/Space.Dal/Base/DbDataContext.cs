using Microsoft.EntityFrameworkCore;
using Space.Dal.Entities;
using Space.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Dal.Base
{
    public class DbDataContext:DbContext, IDbDataContext
    {
        public DbDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ECity> Cities { get; set; }
    }
}
