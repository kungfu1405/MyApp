using Mic.Core.Dal;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Space.Dal.Entities;
using Space.Dal.Interfaces;
using Space.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Dal
{
    public class CityDal : DalBase<IDbDataContext, ECity>, ICity
    {        
        public CityDal(IDbDataContext context) : base(context)
        {

        }
        public async Task<PagingResult<ECity>> GetList(CityFilters filter = null)
        {
            var lst = Context.Cities.AsQueryable();
            if (filter != null)
            {
                lst = lst.Where(e => !filter.CountryId.HasValue || filter.CountryId == 0 || filter.CountryId == e.CountryId)
                    .Where(e => !filter.StateId.HasValue || filter.StateId == 0 || filter.StateId == e.StateId)
                    .Where(e => string.IsNullOrWhiteSpace(filter.Name) || e.Name.Contains(filter.Name));
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.Name);

            var result = new PagingResult<ECity>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (filter == null)
            {
                var defaultPaging = new DatatablePaging { Length = 30, Start = 0 };
                result.Data = await DalUtils.Paging(lst, defaultPaging).ToListAsync();
            }
            else
            {
                result.Data = await DalUtils.Paging(lst, filter.Paging).ToListAsync();
            }

            return result;
        }
    }
}
