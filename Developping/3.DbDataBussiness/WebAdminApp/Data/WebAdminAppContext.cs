using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbData.Entitties;

namespace WebAdminApp.Data
{
    public class WebAdminAppContext : DbContext
    {
        public WebAdminAppContext (DbContextOptions<WebAdminAppContext> options)
            : base(options)
        {
        }

        public DbSet<DbData.Entitties.ECity> ECity { get; set; }
    }
}
