using DbData.Dal.Interfaces;
using DbData.Entities;
using Mic.Core.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class DestinationLanguageDal : DalBase<IDbDataContext, EDestinationLanguage>, IDestinationLanguage
    {
        public DestinationLanguageDal(IDbDataContext context) : base(context)
        {
        }

        public async Task<EDestinationLanguage> Get(Guid destinationId, string langCode, string defaultLang = "")
        {
            var itm = await Context.DestinationLanguages
               .Where(e => e.DestinationId == destinationId && e.LangCode == langCode.Trim().ToLower())               
               .SingleOrDefaultAsync();
            //var itm = await Context.DestinationLanguages
            //    .Where(e => e.DestinationId == destinationId)
            //    .Where(e => e.LangCode == langCode.Trim().ToLower())
            //    .SingleOrDefaultAsync();

            //if (itm == null)
            //{
            //    if (!string.IsNullOrEmpty(defaultLang) && langCode != defaultLang)
            //    {
            //        itm = await Context.DestinationLanguages
            //           .Where(e => e.DestinationId == destinationId)
            //           .Where(e => e.LangCode == defaultLang.Trim().ToLower())
            //           .SingleOrDefaultAsync();
            //    }
            //    if (itm == null)
            //    {
            //        itm = await Context.DestinationLanguages
            //           .Where(e => e.DestinationId == destinationId)
            //           .FirstOrDefaultAsync();
            //    }
            //}
            return itm;
        }
        public async Task<EDestinationLanguage> GetById(Guid destinationId)
        {
            var itm = await Context.DestinationLanguages
                .Where(e => e.DestinationId == destinationId)
                .SingleOrDefaultAsync();
            return itm;
        }
        public async Task<IList<EDestinationLanguage>> GetList(Guid destinationId)
        {
            return await Context.DestinationLanguages
                .Where(e => e.DestinationId == destinationId)
                .ToListAsync();
        }
        public async Task Delete(Guid Id)
        {
            var item = Context.DestinationLanguages.Where(e => e.Id == Id).Single();
            Context.DestinationLanguages.Remove(item);
        }
    }
}
