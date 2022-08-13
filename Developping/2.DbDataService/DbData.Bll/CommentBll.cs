using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class CommentBll : BllDbDataBase
    {
        public CommentBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EComment> Add(EComment entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Comment))
                throw new InvalidInputException("Invalid data");

            return await CommentDao.Add(entry);
        }

        public async Task Edit(EComment entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            await CommentDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");
            await CommentDao.Delete(id);
        }

        public async Task<EComment> Get(Guid id)
        {
            return new Guid().Equals(id) ? null : await CommentDao.Get(id);
        }

        public async Task<PagingResult<EComment>> GetList(CommentFilters filter = null)
        {
            return await CommentDao.GetList(filter);
        }
    }
}
