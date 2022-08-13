using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IAttractionLanguage : IBaseRepository<EAttractionLanguage>
    {
        Task<EAttractionLanguage> Get(Guid attractionId, string langCode, string defaultLang = "");
        Task<EAttractionLanguage> GetById(Guid attractionId);
        Task<IList<EAttractionLanguage>> GetList(Guid attractionId);
    }
}
