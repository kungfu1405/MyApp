using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class CommentDal : DalBase<IDbDataContext, EComment>, IComment
    {
        public CommentDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<EComment> Add(EComment entry)
        {
            entry.Id = Guid.NewGuid();
            entry.CreateDate = DateTime.UtcNow;

            return await base.Add(entry);
        }

        public async Task<PagingResult<EComment>> GetList(CommentFilters filter = null)
        {
            var lst = Context.Comments.AsQueryable();
            if (filter != null && filter.AttractionId.HasValue)
            {
                lst = lst
                    .Where(e => !e.AttractionId.HasValue || e.AttractionId == filter.AttractionId)
                    .Where(e => !e.ExperienceId.HasValue || e.ExperienceId == filter.ExperienceId)
                    .Include(e => e.CommentImages);
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderByDescending(e => e.CreateDate);

            var result = new PagingResult<EComment>();
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
