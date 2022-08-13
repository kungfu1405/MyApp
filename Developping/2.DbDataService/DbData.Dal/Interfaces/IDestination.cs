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
    public interface IDestination : IBaseRepository<EDestination>
    {
        Task<EDestination> Get(string routeUri);
        Task<PagingResult<EDestination>> GetList(DestinationFilters filter = null);
    }
}
