using DbData.Dal;
using DbData.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;
using Mic.Core.Entities;
using DbData.Entities.Models;

namespace DbData.Bll
{
    public class DestinationBll : BllDbDataBase
    {
        public DestinationBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EDestination> Add(EDestination entry)
        {
            //if (string.IsNullOrWhiteSpace(entry.De)
            //    || string.IsNullOrWhiteSpace(entry.FileUrl))
            //    throw new InvalidInputException("Invalid data");
            entry.CityId = entry.CityId == 0 ? null : entry.CityId;
            entry.StateId = entry.StateId == 0 ? null : entry.StateId;

            return await DestinationDao.Add(entry);
        }

        public async Task Edit(EDestination entry)
        {
            entry.CityId = entry.CityId == 0 ? null : entry.CityId;
            entry.StateId = entry.StateId == 0 ? null : entry.StateId;
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            await DestinationDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");
            await DestinationDao.Delete(id);
        }

        public async Task<EDestination> Get(Guid id)
        {
            var result = new Guid().Equals(id) ? null : await DestinationDao.Get(id);
            if (result == null)
                return null;
            result.DestinationLanguages = await DestinationLanguageDao.GetList(result.Id);
            result.DestinationOverviews = await DestinationOverviewDao.GetList(result.Id);
            return result;
        }

        public async Task<EDestination> Get(string routeUri, string langCode = "", string defaultLang = "")
        {
            var result = new Guid().Equals(routeUri) ? null : await DestinationDao.Get(routeUri);
            if (result == null)
                return null;

            // Language
            if (!string.IsNullOrEmpty(langCode))
            {
                result.DestinationLanguage = await DestinationLanguageDao.Get(result.Id, langCode, defaultLang);
            }
            else
                result.DestinationLanguages = await DestinationLanguageDao.GetList(result.Id);

            // Overview
            if(result.DestinationOverviewId.HasValue)
                result.DestinationOverview = await DestinationOverviewDao.Get(result.Id, langCode, defaultLang);

            return result;
        }

        public async Task<PagingResult<EDestination>> GetList(DestinationFilters filter = null)
        {
            var result = await DestinationDao.GetList(filter);
            if (result.TotalRecords > 0)
            {
                var lang = string.IsNullOrEmpty(filter.LangCode) ? filter.DefaultLang : filter.LangCode;
                foreach (var itm in result.Data)
                {
                    itm.DestinationLanguage = await DestinationLanguageDao.Get(itm.Id, filter.LangCode, filter.DefaultLang);
                }
            }
            return result;
        }
    }
}
