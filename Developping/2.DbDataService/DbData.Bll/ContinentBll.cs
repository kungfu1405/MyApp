using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class ContinentBll : BllDbDataBase
    {
        public ContinentBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<IList<EContinent>> All()
        {
            return await ContinentDao.All();
        }
    }
}
