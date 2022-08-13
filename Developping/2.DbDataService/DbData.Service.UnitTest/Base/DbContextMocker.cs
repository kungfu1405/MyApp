using Microsoft.EntityFrameworkCore;
using DbData.Dal;
using Mic.UserDb.Dal;

namespace DbData.Service.UnitTest
{
    public static class DbContextMocker
    {
        public static DbDataContext GetDbContext(string dbName)
        {
            // Create options for DbContext instance
            var dbOptions = new DbContextOptionsBuilder<DbDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new DbDataContext(dbOptions);

            return dbContext;
        }
        public static UserDbContext GetUserContext(string dbName)
        {
            // Create options for DbContext instance
            var dbOptions = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var userContext = new UserDbContext(dbOptions);

            return userContext;
        }
    }
}
