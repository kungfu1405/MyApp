using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IExperienceSession : IBaseRepository<EExperienceSession>
    {
        Task DeleteBy(Guid experienceId);
        Task<EExperienceSession> Get(Guid id, string langCode, string defaultLang = "");
        Task<IList<EExperienceSession>> GetList(Guid? experienceId = null, Guid? sessionId = null, string langCode = "", string defaultLang = "");
    }
}
