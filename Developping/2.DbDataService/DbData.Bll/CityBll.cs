using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class CityBll : BllDbDataBase
    {
        public CityBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<ECity> Add(ECity entry)
        {
            if (string.IsNullOrEmpty(entry.Name)
                || entry.CountryId <= 0
                || entry.StateId <= 0)
                throw new InvalidInputException("Invalid Input");

            // set Country Code = Country ISO-2
            var country = await CountryDao.Get(entry.CountryId);
            if (country == null)
                throw new InvalidInputException("Country not found");
            entry.CountryCode = country.Iso2;

            // set State Code = State ISO-2
            var state = await StateDao.Get(entry.StateId);
            if (state == null)
                throw new InvalidInputException("State not found");
            entry.StateCode = string.IsNullOrEmpty(state.Iso2) ? state.Name : state.Iso2;

            return await CityDao.Add(entry);
        }

        public async Task Edit(ECity entry, bool hasUpdatePassword = false)
        {
            if (entry.Id <= 0)
                throw new InvalidInputException("Invalid ID");

            if (string.IsNullOrEmpty(entry.Name)
                || entry.CountryId <= 0
                || entry.StateId <= 0)
                throw new InvalidInputException("Invalid Input");

            // set Country Code = Country ISO-2
            var country = await CountryDao.Get(entry.CountryId);
            if (country == null)
                throw new InvalidInputException("Country not found");
            entry.CountryCode = country.Iso2;

            // set State Code = State ISO-2
            var state = await StateDao.Get(entry.StateId);
            if (state == null)
                throw new InvalidInputException("State not found");
            entry.StateCode = state.Iso2;

            await CityDao.Edit(entry);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new InvalidInputException("Invalid ID");

            await CityDao.Delete(id);
        }

        public async Task<ECity> Get(int id)
        {
            return id <= 0 ? null : await CityDao.Get(id);
        }

        public async Task<PagingResult<ECity>> GetList(CityFilters filter = null)
        {
            return await CityDao.GetList(filter);
        }
    }
}
