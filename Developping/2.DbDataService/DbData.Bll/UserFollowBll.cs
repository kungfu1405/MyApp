using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.DataTypes;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class UserFollowBll : BllDbDataBase
    {
        public UserFollowBll(IDbDataContext context) : base(context)
        {
        }

        public async Task Add(Guid userId, Guid followingId)
        {
            if (new Guid().Equals(userId) || new Guid().Equals(followingId))
                return;

            await UserFollowDao.Add(new EUserFollow
            {
                UserId = userId,
                UserFollowingId = followingId
            });
        }
        public async Task Delete(Guid userId, Guid followingId)
        {
            if (new Guid().Equals(userId) || new Guid().Equals(followingId))
                return;
            await UserFollowDao.Delete(userId, followingId);
        }

        public async Task<PagingResult<EUserFollow>> GetFollowers(Guid userId, DatatablePaging paging = null)
        {
            if (new Guid().Equals(userId))
                new PagingResult<EUserFollow>();
            return await UserFollowDao.GetFollowers(userId, paging);
        }

        public async Task<PagingResult<EUserFollow>> GetFollowings(Guid userId, DatatablePaging paging = null)
        {
            if (new Guid().Equals(userId))
                new PagingResult<EUserFollow>();
            return await UserFollowDao.GetFollowings(userId, paging);
        }

        public async Task<bool> Exists(Guid userId, Guid followingId)
        {
            if (new Guid().Equals(userId) || new Guid().Equals(followingId))
                return false;
            return await UserFollowDao.Exists(userId, followingId);
        }
    }
}
