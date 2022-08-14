using Space.Dal;
using Space.Dal.Entities;
using Space.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Bussiness
{    
    public class CityBll
    {
        protected readonly IDbDataContext _context;
        private ICity _city;
        protected ICity CityDao { get { return _city ?? (_city = new CityDal(_context)); } }
        public CityBll(IDbDataContext context)
        {
            _context = context;
        }
        public override async Task<ECity> Get(object id)
        {
            return await Context.Cities
                .Where(e => e.Id == (int)id)
                .Include(e => e.Country)
                .Include(e => e.State)
                .SingleOrDefaultAsync();
        }
    }
}
