using DbData.Dal;
using DbData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Service.UnitTest.Base
{
    public class InitDbData
    {
        private readonly DbDataContext context;
        public readonly Random random = new Random();

        public readonly int CountryId;

        public InitDbData(DbDataContext userContext)
        {
            context = userContext;
            CountryId = random.Next();
        }

        public void InitCountry()
        {
            context.Countries.Add(new ECountry
            {
                Id = CountryId,
                Name = "Country Name " + CountryId
            });
            context.SaveChanges();
        }
    }
}
