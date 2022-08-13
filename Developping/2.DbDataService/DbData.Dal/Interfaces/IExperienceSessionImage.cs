using DbData.Entities;
using Mic.Core.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IExperienceSessionImage : IBaseRepository<EExperienceSessionImage>
    {
        Task DeleteBy(Guid sessionId);
        Task DeleteAll(Guid experienceId);
        Task<IList<EExperienceSessionImage>> GetList(Guid experienceId, Guid experienceSessionId);
    }
}
