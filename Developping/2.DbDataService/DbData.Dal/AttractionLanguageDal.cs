using DbData.Dal.Interfaces;
using DbData.Entities;
using Mic.Core.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mic.Core.Exceptions;

namespace DbData.Dal
{
    public class AttractionLanguageDal : DalBase<IDbDataContext, EAttractionLanguage>, IAttractionLanguage
    {
        public AttractionLanguageDal(IDbDataContext context) : base(context)
        {
        }
        public override async Task<EAttractionLanguage> Add(EAttractionLanguage entry)
        {
            entry.Id = Guid.NewGuid();
            return await base.Add(entry);
        }

        public override async Task Edit(EAttractionLanguage entry)
        {

            var itm = await Context.AttractionLanguages.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Attraction Language does not exists."
                    , new Exception($"Attraction Language ({entry.Id}) does not exists."));

            itm.Name = entry.Name;
            itm.Description = entry.Description;
            itm.AttractionId= entry.AttractionId;
            
            await Context.SaveChangesAsync();
        }
        public async Task<EAttractionLanguage> Get(Guid attractionId, string langCode, string defaultLang = "")
        {
            var itm = await Context.AttractionLanguages
                .Where(e => e.AttractionId == attractionId && e.LangCode == langCode.Trim().ToLower())
                //.Where(e => e.LangCode == langCode.Trim().ToLower())
                .SingleOrDefaultAsync();
            //if (itm == null)
            //{
            //    if (!string.IsNullOrEmpty(defaultLang) && langCode != defaultLang)
            //    {
            //        itm = await Context.AttractionLanguages
            //           .Where(e => e.AttractionId == attractionId)
            //           .Where(e => e.LangCode == defaultLang.Trim().ToLower())
            //           .SingleOrDefaultAsync();
            //    }
            //    if (itm == null)
            //    {
            //        itm = await Context.AttractionLanguages
            //           .Where(e => e.AttractionId == attractionId)
            //           .FirstOrDefaultAsync();
            //    }
            //}
            return itm;
        }
        public async Task<EAttractionLanguage> GetById(Guid attractionId)
        {
            var itm = await Context.AttractionLanguages
                .Where(e => e.AttractionId == attractionId)                
                .SingleOrDefaultAsync();
            return itm;
        }

        public async Task<IList<EAttractionLanguage>> GetList(Guid attractionId)
        {
            return await Context.AttractionLanguages
                .Where(e => e.AttractionId == attractionId)
                .ToListAsync();
        }
        public async Task Delete(Guid Id)
        {
            var item = Context.AttractionLanguages.Where(e => e.Id == Id).Single();
            Context.AttractionLanguages.Remove(item);
        }
    }
}
