using Microsoft.EntityFrameworkCore;

namespace DbData.Dal.MsSql
{
    public class MsSqlDbDataContext: DbDataContext, IDbDataContext
    {
        public MsSqlDbDataContext(DbContextOptions<MsSqlDbDataContext> options) : base(options)
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
