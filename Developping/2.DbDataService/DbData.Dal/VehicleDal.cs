using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class VehicleDal : DalBase<IDbDataContext, EVehicle>, IVehicle
    {
        public VehicleDal(IDbDataContext context) : base(context)
        {

        }

        public override async Task<EVehicle> Add(EVehicle entry)
        {
            await CheckExists(entry);
            return await base.Add(entry);
        }

        public override async Task Edit(EVehicle entry)
        {
            await CheckExists(entry);

            var itm = await Context.Vehicles.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Vehicle does not exists."
                    , new Exception($"Vehicle ({entry.Id}) does not exists."));

            itm.Name = entry.Name;
            itm.CssIcon = entry.CssIcon;
            itm.Ordinal = entry.Ordinal;         
            await Context.SaveChangesAsync();
        }

        public override async Task<EVehicle> Get(object id)
        {
            return await Context.Vehicles
                .Where(e => e.Id == (Guid)id)
                //.Include(e => e.Country)
                //.Include(e => e.State)
                .SingleOrDefaultAsync();
        }

        public async Task<PagingResult<EVehicle>> GetList(VehicleFilter filter = null)
        {
            var lst = Context.Vehicles.AsQueryable();
            if (filter != null)
            {
                lst = lst
                    .Where(e => string.IsNullOrWhiteSpace(filter.Name) || e.Name.Contains(filter.Name));
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.Name);

            var result = new PagingResult<EVehicle>();
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

        private async Task CheckExists(EVehicle entry)
        {
            var query = Context.Vehicles
                .Where(e =>string.IsNullOrEmpty(entry.Id.ToString())  || entry.Id != e.Id)
                ;

            if (await query.AnyAsync(e => e.Name == entry.Name))
                throw new DuplicatedDataException($"City Name {entry.Name} existed.");
        }
    }
}
