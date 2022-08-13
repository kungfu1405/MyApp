using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IDestinationLanguage : IBaseRepository<EDestinationLanguage>
    {
        Task<EDestinationLanguage> Get(Guid destinationId, string langCode, string defaultLang = "");
        Task<IList<EDestinationLanguage>> GetList(Guid destinationId);
        Task<EDestinationLanguage> GetById(Guid destinationId);
    }
}
