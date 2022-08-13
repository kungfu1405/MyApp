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
    public class StateDal : DalBase<IDbDataContext, EState>, IState
    {
        public StateDal(IDbDataContext context) : base(context)
        {

        }

        public override async Task<EState> Add(EState entry)
        {
            await CheckExists(entry);
            return await base.Add(entry);
        }

        public override async Task Edit(EState entry)
        {
            await CheckExists(entry);

            var itm = await Context.States.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("State does not exists."
                    , new Exception($"State ({entry.Id}) does not exists."));

            itm.Name = entry.Name;
            itm.CountryId = entry.CountryId;
            itm.CountryCode = entry.CountryCode;
            itm.FipsCode = entry.FipsCode;
            itm.Iso2 = entry.Iso2;
            itm.WikiDataId = entry.WikiDataId;

            await Context.SaveChangesAsync();
        }

        public override async Task<EState> Get(object id)
        {
            return await Context.States
                .Where(e => e.Id == (int)id)
                .Include(e => e.Country)
                .SingleOrDefaultAsync();
        }

        public async Task<PagingResult<EState>> GetList(StateFilters filter = null)
        {
            var lst = Context.States.AsQueryable();
            if (filter != null)
            {
                lst = lst.Where(e => !filter.CountryId.HasValue || filter.CountryId == 0 || filter.CountryId == e.CountryId)
                    .Where(e => string.IsNullOrWhiteSpace(filter.Name)
                    || e.Name.Contains(filter.Name)
                    || e.Iso2 == filter.Name);
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.Name);

            var result = new PagingResult<EState>();
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

        private async Task CheckExists(EState entry)
        {
            var query = Context.States
                .Where(e => entry.Id == 0 || entry.Id != e.Id)
                .Where(e => e.CountryId == entry.CountryId);

            if (await query.AnyAsync(e => e.Name == entry.Name))
                throw new DuplicatedDataException($"State Name {entry.Name} existed.");

            if (await query.AnyAsync(e => !string.IsNullOrEmpty(entry.Iso2) && e.Iso2 == entry.Iso2))
                throw new DuplicatedDataException($"State ISO-2 {entry.Iso2} existed.");
        }
    }
}
