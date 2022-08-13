using System;
using Microsoft.EntityFrameworkCore;
using Space.Dal.Base;
using Space.Dal.Interfaces;

namespace Space.Dal.MsSql
{
    public class MsSqlDbDataContext : DbDataContext , IDbDataContext
    {
        public MsSqlDbDataContext(DbContextOptions<MsSqlDbDataContext> options) : base(options)
        {

        }
    }
}
