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
    public class CountryDal : DalBase<IDbDataContext, ECountry>, ICountry
    {
        public CountryDal(IDbDataContext context) : base(context)
        {

        }

        public override async Task<ECountry> Add(ECountry entry)
        {
            await CheckExists(entry);
            if (entry.ContinentId == 0)
                entry.ContinentId = null;
            entry.Iso2 = entry.Iso2.ToUpper();
            entry.Iso3 = entry.Iso3.ToUpper();
            return await base.Add(entry);
        }

        public override async Task Edit(ECountry entry)
        {
            await CheckExists(entry);

            var itm = await Context.Countries.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Country does not exists."
                    , new Exception($"Country ({entry.Id}) does not exists."));

            if (entry.ContinentId == 0)
                entry.ContinentId = null;
            itm.ContinentId = entry.ContinentId;
            itm.Name = entry.Name;
            itm.Iso2 = entry.Iso2.ToUpper();
            itm.Iso3 = entry.Iso3.ToUpper();
            itm.Native = entry.Native;
            itm.Capital = entry.Capital;
            itm.Currency = entry.Currency;
            itm.PhoneCode = entry.PhoneCode;
            itm.Emoji = entry.Emoji;
            itm.EmojiU = entry.EmojiU;
            itm.WikiDataId = entry.WikiDataId;

            await Context.SaveChangesAsync();
        }

        public override async Task<ECountry> Get(object id)
        {
            return await Context.Countries
                .Where(e => e.Id == (int)id)
                .Include(e => e.Continent)
                .SingleOrDefaultAsync();
        }

        public async Task<PagingResult<ECountry>> GetList(CountryFilters filter = null)
        {
            var lst = Context.Countries.AsQueryable();
            if (filter != null)
            {
                lst = lst.Where(e=> !filter.ContinentId.HasValue || filter.ContinentId == 0 || filter.ContinentId == e.ContinentId)
                    .Where(e => string.IsNullOrWhiteSpace(filter.Name) 
                    || e.Name.Contains(filter.Name)
                    || e.Native.Contains(filter.Name)
                    || e.Iso2 == filter.Name
                    || e.Iso3 == filter.Name);
            }
            lst = lst.Include(e => e.Continent);

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.Name);

            var result = new PagingResult<ECountry>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (filter == null)
            {
                result.Data = await lst.ToListAsync();
            }
            else
            {
                result.Data = await DalUtils.Paging(lst, filter.Paging).ToListAsync();
            }

            return result;
        }

        private async Task CheckExists(ECountry entry)
        {
            var query = Context.Countries.Where(e => entry.Id == 0 || entry.Id != e.Id);

            if (await query.AnyAsync(e => e.Name == entry.Name))
                throw new DuplicatedDataException($"Country Name {entry.Name} existed.");

            if(await query.AnyAsync(e=> !string.IsNullOrEmpty(entry.Iso2) && e.Iso2 == entry.Iso2))
                throw new DuplicatedDataException($"Country ISO-2 {entry.Iso2} existed.");

            if (await query.AnyAsync(e => !string.IsNullOrEmpty(entry.Iso3) && e.Iso3 == entry.Iso3))
                throw new DuplicatedDataException($"Country ISO-3 {entry.Iso3} existed.");
        }
    }
}
