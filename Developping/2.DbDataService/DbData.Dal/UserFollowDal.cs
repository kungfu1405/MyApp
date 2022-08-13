using DbData.Dal.Interfaces;
using DbData.Entities;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class UserFollowDal : DalBase<IDbDataContext, EUserFollow>, IUserFollow
    {
        public UserFollowDal(IDbDataContext context) : base(context)
        {
        }

        public override Task Edit(EUserFollow entry)
        {
            throw new NotSupportedException();
        }

        public override async Task Delete(object id)
        {
            var lst = Context.UserFollows.Where(e => e.UserId == (Guid)id);
            if (await lst.AnyAsync())
            {
                Context.UserFollows.RemoveRange(lst);
                await Context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid userId, Guid followingId)
        {
            var itm = await Context.UserFollows
                .SingleOrDefaultAsync(e => e.UserId == userId && e.UserFollowingId == followingId);
            if (itm == null)
                return;

            Context.UserFollows.Remove(itm);
            await Context.SaveChangesAsync();
        }

        public async Task<PagingResult<EUserFollow>> GetFollowers(Guid userId, DatatablePaging paging = null)
        {
            var lst = Context.UserFollows
                .Where(e => e.UserId == userId)
                .OrderBy(e => e.UserId);

            var result = new PagingResult<EUserFollow>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (paging == null)
            {
                paging = new DatatablePaging
                {
                    Start = 0,
                    Length = 30
                };
            }
            result.Data = await DalUtils.Paging(lst, paging).ToListAsync();
            return result;
        }

        public async Task<PagingResult<EUserFollow>> GetFollowings(Guid userId, DatatablePaging paging = null)
        {
            var lst = Context.UserFollows
                .Where(e => e.UserFollowingId == userId)
                .OrderBy(e => e.UserFollowingId);

            var result = new PagingResult<EUserFollow>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (paging == null)
            {
                paging = new DatatablePaging
                {
                    Start = 0,
                    Length = 30
                };
            }
            result.Data = await DalUtils.Paging(lst, paging).ToListAsync();
            return result;
        }

        public async Task<bool> Exists(Guid userId, Guid followingId)
        {
            return await Context.UserFollows
                .AnyAsync(e => e.UserId == userId && e.UserFollowingId == followingId);
        }
            
    }
}
