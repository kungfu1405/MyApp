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
    public class AttPropertyDal : DalBase<IDbDataContext, EAttProperty> , IAttProperty
    {
        public AttPropertyDal(IDbDataContext context) : base(context)
        {

        }
        public override async Task<EAttProperty> Add(EAttProperty entry)
        {
            //entry.Id = Guid.NewGuid();
            return await base.Add(entry);
        }
        public override async Task Edit(EAttProperty entry)
        {
          //  await CheckExists(entry);

            var itm = await Context.AttProperties.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Att property does not exists."
                    , new Exception($"Att property ({entry.Id}) does not exists."));

            itm.Name = entry.Name;
            itm.PropertyGroup = entry.PropertyGroup;
            itm.DataType = entry.DataType;
            itm.Description = entry.Description;
            itm.CssIcon = entry.CssIcon;
            itm.Ordinal = entry.Ordinal;
            
            await Context.SaveChangesAsync();
        }

        public async Task<EAttProperty> Get(Guid id)
        {
          return await Context.AttProperties.Where(e =>e.Id == id).SingleOrDefaultAsync();         
        }
        public async Task<PagingResult<EAttProperty>> GetList(AttPropertyFillter filter = null)
        {
           
            var lst = Context.AttProperties.AsQueryable();
            //if (filter != null)
            //{
            //    lst = lst.Where(e => !string.IsNullOrEmpty(filter.Id.ToString()) || filter.Id == e.Id)
            //        .Where(e => !string.IsNullOrEmpty(filter.Name.ToString()) || filter.Name == e.Name)  ;
            //}

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.Name);

            var result = new PagingResult<EAttProperty>();
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
        private async Task CheckExists(EAttProperty entry)
        {
            var query = Context.AttProperties
                .Where(e => !string.IsNullOrEmpty(entry.Id.ToString()) || entry.Id != e.Id);
                //.Where(e => e.CountryId == entry.CountryId)
                //.Where(e => e.StateId == entry.StateId);

            if (await query.AnyAsync(e => e.Name == entry.Name))
                throw new DuplicatedDataException($"Att property Name {entry.Name} existed.");
        }
        public async Task Delete(Guid Id)
        {
            var item = Context.AttProperties.Where(e => e.Id == Id).Single();          
             Context.AttProperties.Remove(item);
        }
    }
}
