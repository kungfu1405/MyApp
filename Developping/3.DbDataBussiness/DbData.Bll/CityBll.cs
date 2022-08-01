using DbData.Dal;
using DbData.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class CityBll
    {      
        CityDal cityDal;
        public DbDataContext Context { get; set; }
        public CityBll()
        {
            cityDal = new CityDal();
            cityDal._context = Context;
        }
        public async Task<ECity> Get(object id)
        {
            //return await Context.Cities
            //    .Where(e => e.Id == (int)id)
            //    .Include(e => e.Country)
            //    .Include(e => e.State)
            //    .SingleOrDefaultAsync();
            return await cityDal.Get(id);
        }
        public async Task<IList<ECity>> GetAll()
        {
            return await cityDal.GetAll();
        }

    }
}
