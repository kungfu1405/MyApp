using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IDestinationOverview : IBaseRepository<EDestinationOverview>
    {
        Task<EDestinationOverview> Get(Guid destinationId, string langCode, string defaultLang = "");
        Task<IList<EDestinationOverview>> GetList(Guid destinationId);
    }
}
