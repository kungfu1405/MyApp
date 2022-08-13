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
    public class UserProfileBll : BllDbDataBase
    {
        public UserProfileBll(IDbDataContext context) : base(context)
        {
        }
        public async Task<EUserProfile> Add(EUserProfile entry)
        {
            return await UserProfileDao.Add(entry);
        }

        public async Task Edit(EUserProfile entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid ID");

            await UserProfileDao.Edit(entry);
        }
        public async Task Delete(Guid id)
        {
            if (!new Guid().Equals(id))
                await UserProfileDao.Delete(id);

        }

        public async Task UpdateCount(EnumStatisticType statisticType, Guid id, int number)
        {
            if (new Guid().Equals(id) || number == 0)
                return;
            await UserProfileDao.UpdateCount(statisticType, id, number);
        }

        public async Task<EUserProfile> Get(Guid id)
        {
            if (new Guid().Equals(id))
                return null;

            return await UserProfileDao.Get(id);
        }
    }
}
