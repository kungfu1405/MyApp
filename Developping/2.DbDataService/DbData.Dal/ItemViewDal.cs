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
    public class ItemViewDal : DalBase<IDbDataContext, EItemView>, IItemView
    {
        public ItemViewDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<EItemView> Add(EItemView entry)
        {
            entry.Id = Guid.NewGuid();
            entry.CreatedTime = DateTime.UtcNow;

            return await base.Add(entry);
        }

        public override async Task Edit(EItemView entry)
        {
            var itm = await Context.ItemViews.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("ItemView does not exists."
                    , new Exception($"ItemView ({entry.Id}) does not exists."));
                        
            itm.Title = entry.Title;
            itm.ImgThumb = entry.ImgThumb;
            itm.Status = entry.Status;

            await Context.SaveChangesAsync();
        }

        public override async Task<EItemView> Get(object id)
        {
            return await Context.ItemViews
                .Where(e => e.Id == (Guid)id)
                .SingleOrDefaultAsync();
        }

        public async Task<PagingResult<EItemView>> GetList(ItemViewFilters filter = null)
        {
            var lst = Context.ItemViews.AsQueryable();

            if (filter != null)
            {
                lst = lst
                    .Where(e => string.IsNullOrEmpty(filter.Name));

            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderBy(e => e.TopView);

            var result = new PagingResult<EItemView>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (filter == null)
            {
                filter.Paging = new DatatablePaging
                {
                    Start = 0,
                    Length = 30
                };
            }
            result.Data = await DalUtils.Paging(lst, filter.Paging).ToListAsync();
            return result;
        }
    }
}
