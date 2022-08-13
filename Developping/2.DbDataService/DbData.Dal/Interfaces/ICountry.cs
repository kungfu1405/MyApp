using Mic.Core.Dal;
using DbData.Entities;
using System.Threading.Tasks;
using Mic.Core.Entities;
using DbData.Entities.Models;

namespace DbData.Dal.Interfaces
{
    public interface ICountry : IBaseRepository<ECountry>
    {
        Task<PagingResult<ECountry>> GetList(CountryFilters filter = null);
    }
}
