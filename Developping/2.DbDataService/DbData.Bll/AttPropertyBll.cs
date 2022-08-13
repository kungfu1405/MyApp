using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class AttPropertyBll : BllDbDataBase
    {
        public AttPropertyBll(IDbDataContext context) : base(context)
        {

        }
        public async Task<EAttProperty> Add(EAttProperty entry)
        {
          
            return await AttPropertyDao.Add(entry);
        }
        public async Task<EAttProperty> Get(Guid id)
        {
            //
            return await AttPropertyDao.Get(id);
        }
        public async Task<PagingResult<EAttProperty>> GetList(AttPropertyFillter filter = null)
        {
            return await AttPropertyDao.GetList(filter);
        }
        public async Task Edit(EAttProperty entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            await AttPropertyDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");
            await AttPropertyDao.Delete(id);
        }
    }
}
