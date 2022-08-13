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
    public class TagDal : DalBase<IDbDataContext, ETag>, ITag
    {
        public TagDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<ETag> Add(ETag entry)
        {
            var itm = await Context.Tags.SingleOrDefaultAsync(e => e.Name == entry.Name);
            if (itm == null)
            {
                entry.Id = Guid.NewGuid();
                return await base.Add(entry);
            }
            return itm;
        }

        public async Task<PagingResult<ETag>> GetList(TagFilters filter = null)
        {
            var lst = Context.Tags.AsQueryable();
            if (filter != null)
            {
                lst = lst.Where(e => string.IsNullOrWhiteSpace(filter.Name));

                if (filter.ExperienceId.HasValue)
                {
                    var lstTag = await Context.ExperienceTags.Where(e => e.ExperienceId.Equals(filter.ExperienceId.Value)).Select(e => e.TagId).ToListAsync();
                    lst = lst.Where(e => lstTag.Contains(e.Id));
                }
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderByDescending(e => e.Name);

            var result = new PagingResult<ETag>();
            // Totals
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (filter == null)
            {
                DatatablePaging datatablePaging = new DatatablePaging();
                datatablePaging.Length = 30;
                datatablePaging.Start = 0;
                result.Data = await DalUtils.Paging(lst, datatablePaging).ToListAsync();
            }
            else
            {
                result.Data = await DalUtils.Paging(lst, filter.Paging).ToListAsync();
            }

            return result;
        }

        public async Task<List<ETag>> GetListTag(TagFilters filter = null)
        {
            var lst = Context.Tags.AsQueryable();
            if (filter != null)
            {
                lst = lst.Where(e => string.IsNullOrWhiteSpace(filter.Name));

                if (filter.ExperienceId.HasValue)
                {
                    var lstTag = await Context.ExperienceTags.Where(e => e.ExperienceId.Equals(filter.ExperienceId.Value)).Select(e => e.TagId).ToListAsync();
                    lst = lst.Where(e => lstTag.Contains(e.Id));
                }
            }

            return await lst.ToListAsync();
        }
    }
}
