using DbData.Dal;
using DbData.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class DestinationLanguageBll : BllDbDataBase
    {
        public DestinationLanguageBll(IDbDataContext context) : base(context)
        {

        }
        public async Task<EDestinationLanguage> Add(EDestinationLanguage entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Name))
                throw new InvalidInputException("Invalid data");

            return await DestinationLanguageDao.Add(entry);
        }
        public async Task Edit(EDestinationLanguage entry)
        {
            await DestinationLanguageDao.Edit(entry);
        }
        public async Task<IList<EDestinationLanguage>> GetList(Guid id)
        {
            return await DestinationLanguageDao.GetList(id);
        }
        public async Task<EDestinationLanguage> Get(string id, string langCode, string defaultLang = "")
        {
            return await DestinationLanguageDao.Get(new Guid(id), langCode, defaultLang);
        }
        public async Task<EDestinationLanguage> GetById(string id)
        {
            return await DestinationLanguageDao.GetById(new Guid(id));
        }
        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid ID");
            await DestinationLanguageDao.Delete(id);
        }

    }
}
