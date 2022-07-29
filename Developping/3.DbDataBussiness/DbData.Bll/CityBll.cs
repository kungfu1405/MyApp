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
      //  private  DbDataContext _context = new DbDataContext();
        private readonly CityDal context ;

        public CityBll()
        {            
            context = new CityDal();
        }
        public async Task<ECity> Get(object id)
        {
            //return await Context.Cities
            //    .Where(e => e.Id == (int)id)
            //    .Include(e => e.Country)
            //    .Include(e => e.State)
            //    .SingleOrDefaultAsync();
            return await context.Get(id);
        }

    }
}
