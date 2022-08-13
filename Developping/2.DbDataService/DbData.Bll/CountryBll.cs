using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class CountryBll : BllDbDataBase
    {
        public CountryBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<ECountry> Add(ECountry entry)
        {
            if (string.IsNullOrEmpty(entry.Name))
                throw new InvalidInputException("Invalid Input");

            return await CountryDao.Add(entry);
        }

        public async Task Edit(ECountry entry, bool hasUpdatePassword = false)
        {
            if (entry.Id <= 0)
                throw new InvalidInputException("Invalid ID");

            if (string.IsNullOrEmpty(entry.Name))
                throw new InvalidInputException("Invalid Input");

            await CountryDao.Edit(entry);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new InvalidInputException("Invalid ID");

            await CountryDao.Delete(id);
        }

        public async Task<ECountry> Get(int id)
        {
            return id <= 0 ? null : await CountryDao.Get(id);
        }

        public async Task<PagingResult<ECountry>> GetList(CountryFilters filter = null)
        {
            return await CountryDao.GetList(filter);
        }
    }
}
