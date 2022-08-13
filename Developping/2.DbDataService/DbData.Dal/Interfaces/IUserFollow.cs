using DbData.Entities;
using Mic.Core.Dal;
using Mic.Core.Entities;
using System;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IUserFollow : IBaseRepository<EUserFollow>
    {
        Task Delete(Guid userId, Guid followingId);
        Task<PagingResult<EUserFollow>> GetFollowers(Guid userId, DatatablePaging paging = null);
        Task<PagingResult<EUserFollow>> GetFollowings(Guid userId, DatatablePaging paging = null);
        Task<bool> Exists(Guid userId, Guid followingId);
    }
}
