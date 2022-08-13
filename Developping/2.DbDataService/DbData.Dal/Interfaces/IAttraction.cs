using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using System.Threading.Tasks;

namespace DbData.Dal.Interfaces
{
    public interface IAttraction : IBaseRepository<EAttraction>
    {
        Task<EAttraction> Get(string routeUri);
        Task<PagingResult<EAttraction>> GetList(AttractionFilters filter = null);
    }
}
