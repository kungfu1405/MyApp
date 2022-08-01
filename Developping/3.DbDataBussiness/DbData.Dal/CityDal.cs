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
        public DbDataContext _context { get; set; }
        //private readonly DbDataContext _context = new DbDataContext();
        public CityDal()
        {            
        }
        public async Task<ECity> Get(object id)
        {
            return await _context.Cities
                .Where(e => e.Id == (int)id)                
                .SingleOrDefaultAsync();
        }
        public async Task<List<ECity>> GetAll()
        {
            return await _context.Cities.ToListAsync();
        }
    }
}
