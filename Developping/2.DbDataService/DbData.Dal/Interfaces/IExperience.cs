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
    public interface IExperience : IBaseRepository<EExperience>
    {
        Task AddAttraction(EExperienceAttractionRef entry);
        Task AddExperienceTag(EExperienceTag entry);
        Task<EExperience> Get(string routeUri);
        Task<PagingResult<EExperience>> GetList(ExperienceFilters filter = null);
    }
}
