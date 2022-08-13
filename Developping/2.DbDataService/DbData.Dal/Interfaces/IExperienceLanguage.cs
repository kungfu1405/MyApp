using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IExperienceLanguage : IBaseRepository<EExperienceLanguage>
    {
        Task<EExperienceLanguage> Get(Guid experienceId, string langCode, string defaultLang = "");
        Task<IList<EExperienceLanguage>> GetList(Guid experienceId);
    }
}
