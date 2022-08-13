using Mic.Core.Dal;
using Mic.Core.Exceptions;
using DbData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DbData.Dal.Interfaces;
using Mic.Core.Entities;
using DbData.Entities.Models;

namespace DbData.Dal
{
    public class CityDal : DalBase<IDbDataContext, ECity>, ICity
    {
        public CityDal(IDbDataContext context) : base(context)
        {

        }

        public override async Task<ECity> Add(ECity entry)
        {
            await CheckExists(entry);
            return await base.Add(entry);
        }

        public override async Task Edit(ECity entry)
        {
            await CheckExists(entry);

            var itm = await Context.Cities.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("City does not exists."
                    , new Exception($"City ({entry.Id}) does not exists."));

            itm.Name = entry.Name;
            itm.CountryId = entry.CountryId;
            itm.CountryCode = entry.CountryCode;
            itm.StateId = entry.StateId;
            itm.StateCode = entry.StateCode;
            itm.Latitude = entry.Latitude;
            itm.Longitude = entry.Longitude;
            itm.WikiDataId = entry.WikiDataId;

            await Context.SaveChangesAsync();
        }

        public override async Task<ECity> Get(object id)
        {
            return await Context.Cities
                .Where(e => e.Id == (int)id)
                .Include(e => e.Country)
                .Include(e => e.State)
                .SingleOrDefaultAsync();
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

        private async Task CheckExists(ECity entry)
        {
            var query = Context.Cities
                .Where(e => entry.Id == 0 || entry.Id != e.Id)
                .Where(e => e.CountryId == entry.CountryId)
                .Where(e => e.StateId == entry.StateId);

            if (await query.AnyAsync(e => e.Name == entry.Name))
                throw new DuplicatedDataException($"City Name {entry.Name} existed.");
        }
    }
}
