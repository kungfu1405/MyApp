using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class StateBll : BllDbDataBase
    {
        public StateBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EState> Add(EState entry)
        {
            if (string.IsNullOrEmpty(entry.Name)
                || entry.CountryId <= 0)
                throw new InvalidInputException("Invalid Input");

            // set Country Code = Country ISO-2
            var country = await CountryDao.Get(entry.CountryId);
            if(country == null)
                throw new InvalidInputException("Country not found");
            entry.CountryCode = country.Iso2;

            return await StateDao.Add(entry);
        }

        public async Task Edit(EState entry, bool hasUpdatePassword = false)
        {
            if (entry.Id <= 0)
                throw new InvalidInputException("Invalid ID");

            if (string.IsNullOrEmpty(entry.Name)
                || entry.CountryId <= 0)
                throw new InvalidInputException("Invalid Input");

            // set Country Code = Country ISO-2
            var country = await CountryDao.Get(entry.CountryId);
            if (country == null)
                throw new InvalidInputException("Country not found");
            entry.CountryCode = country.Iso2;

            await StateDao.Edit(entry);
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
                throw new InvalidInputException("Invalid ID");

            await StateDao.Delete(id);
        }

        public async Task<EState> Get(int id)
        {
            return id <= 0 ? null : await StateDao.Get(id);
        }

        public async Task<PagingResult<EState>> GetList(StateFilters filter = null)
        {
            return await StateDao.GetList(filter);
        }
    }
}
