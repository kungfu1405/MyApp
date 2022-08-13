using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class AttractionBll : BllDbDataBase
    {
        public AttractionBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EAttraction> Add(EAttraction entry)
        {
            if (string.IsNullOrWhiteSpace(entry.DefaultName))
                throw new InvalidInputException("Invalid data");

            return await AttractionDao.Add(entry);
        }

        public async Task Edit(EAttraction entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            await AttractionDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");
            await AttractionDao.Delete(id);
        }

        public async Task<EAttraction> GetBy(Guid id)
        {
            return new Guid().Equals(id) ? null : await AttractionDao.Get(id);
        }

        public async Task<EAttraction> Get(string routeUri, string langCode = "", string defaultLang = "")
        {
            var result = new Guid().Equals(routeUri) ? null : await AttractionDao.Get(routeUri);
            if (result == null)
                return null;

            // Language
            if (!string.IsNullOrEmpty(langCode))
            {
                result.AttractionLanguage = await AttractionLanguageDao.Get(result.Id, langCode, defaultLang);
            }
            else
                result.AttractionLanguages = await AttractionLanguageDao.GetList(result.Id);

            return result;
        }

        public async Task<PagingResult<EAttraction>> GetList(AttractionFilters filter = null)
        {
            var result = await AttractionDao.GetList(filter);
            if (result.TotalRecords > 0)
            {
                var lang = string.IsNullOrEmpty(filter.LangCode) ? filter.DefaultLang : filter.LangCode;
                foreach (var itm in result.Data)
                {
                    itm.AttractionLanguage = await AttractionLanguageDao.Get(itm.Id, filter.LangCode, filter.DefaultLang);
                }
            }
            return result;
        }
    }
}
