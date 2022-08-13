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
    public class AttractionLanguageBll: BllDbDataBase
    {
        public AttractionLanguageBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EAttractionLanguage> Add(EAttractionLanguage entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Name))
                throw new InvalidInputException("Invalid data");

            return await AttractionLanguageDao.Add(entry);
        }
        public async Task Edit(EAttractionLanguage entry)
        {
            await AttractionLanguageDao.Edit(entry);
        }
        public async Task<IList<EAttractionLanguage>> GetList(Guid id)
        {
            return await AttractionLanguageDao.GetList(id);
        }
        public async Task<EAttractionLanguage> Get(string id ,string langCode,string defaultLang="")
        {
            return await AttractionLanguageDao.Get(new Guid(id),langCode, defaultLang);                
        }
        public async Task<EAttractionLanguage> GetById(string id)
        {
            return await AttractionLanguageDao.GetById(new Guid(id));
        }
        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid ID");
            await AttractionLanguageDao.Delete(id);
        }

    }
}
