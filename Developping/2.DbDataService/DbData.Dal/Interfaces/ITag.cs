using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface ITag : IBaseRepository<ETag>
    {
        Task<PagingResult<ETag>> GetList(TagFilters filter = null);
        Task<List<ETag>> GetListTag(TagFilters filter = null);
    }
}
