using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IUserProfile : IBaseRepository<EUserProfile>
    {
        Task UpdateCount(EnumStatisticType statisticType, Guid id, int number);
    }
}
