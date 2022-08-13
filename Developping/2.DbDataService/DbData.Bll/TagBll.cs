using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class TagBll : BllDbDataBase
    {
        public TagBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<ETag> Add(ETag entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Name))
                throw new InvalidInputException("Invalid data");

            return await TagDao.Add(entry);
        }

        public async Task Edit(ETag entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            await TagDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");
            await TagDao.Delete(id);
        }

        public async Task<ETag> Get(Guid id)
        {
            return new Guid().Equals(id) ? null : await TagDao.Get(id);
        }

        public async Task<PagingResult<ETag>> GetList(TagFilters filter = null)
        {
            return await TagDao.GetList(filter);
        }
    }
}
