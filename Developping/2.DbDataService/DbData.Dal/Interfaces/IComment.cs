using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IComment : IBaseRepository<EComment>
    {
        Task<PagingResult<EComment>> GetList(CommentFilters filter = null);
    }
}
