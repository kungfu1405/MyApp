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
    public class ItemViewBll : BllDbDataBase
    {
        public ItemViewBll(IDbDataContext context) : base(context)
        {
        }
        public async Task<EItemView> Add(EItemView entry)
        {          

            var result = await ItemViewDao.Add(entry);

            return result;
        }

        public async Task Edit(EItemView entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            var itemView = await ItemViewDao.Get(entry.Id);
            if (itemView == null)
                throw new DataNotFoundException("Item not found");

            await ItemViewDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");

            var itemView = await ItemViewDao.Get(id);
            if (itemView == null)
                throw new DataNotFoundException("Item not found");

            await ItemViewDao.Delete(id);
        }

        public async Task<PagingResult<EItemView>> GetList(ItemViewFilters filter = null)
        {
            return await ItemViewDao.GetList(filter);
        }
    }
}
