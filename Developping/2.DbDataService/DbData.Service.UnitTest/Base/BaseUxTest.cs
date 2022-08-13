using DbData.Dal;
using Force.Crc32;
using Mic.UserDb.Dal;
using System;
using System.Text;

namespace DbData.Service.UnitTest
{
    public class BaseUxTest : IDisposable
    {
        protected readonly DbDataContext dbContext;
        protected readonly UserDbContext userContext;
        protected Random random;

        public BaseUxTest(string contextName)
        {
            random = new Random();
            dbContext = DbContextMocker.GetDbContext(contextName);
            userContext = DbContextMocker.GetUserContext(contextName);
        }

        public string GetCrcId(string text)
        {
            var result = "";
            var data = Encoding.ASCII.GetBytes(text);

            var crc32 = new Crc32Algorithm();
            foreach (byte b in crc32.ComputeHash(data))
                result += b.ToString("x2").ToLower();
            return result;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
