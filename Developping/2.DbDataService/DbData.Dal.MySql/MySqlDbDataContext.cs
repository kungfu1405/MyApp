using Microsoft.EntityFrameworkCore;

namespace DbData.Dal.MySql
{
    public class MySqlDbDataContext: DbDataContext, IDbDataContext
    {
        public MySqlDbDataContext(DbContextOptions<MySqlDbDataContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
