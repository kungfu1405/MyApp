using DbData.Entitties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class CityDal
    {
        private readonly DbDataContext _context;
        //private readonly DbDataContext _context = new DbDataContext();
        public CityDal()
        {
            //_context = context;
        }
        public async Task<ECity> Get(object id)
        {
            return await _context.Cities
                .Where(e => e.Id == (int)id)                
                .SingleOrDefaultAsync();
        }
    }
}
