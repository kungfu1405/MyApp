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
    public class ExperienceLanguageDal : DalBase<IDbDataContext, EExperienceLanguage>, IExperienceLanguage
    {
        public ExperienceLanguageDal(IDbDataContext context) : base(context)
        {
        }

        public async Task<EExperienceLanguage> Get(Guid experienceId, string langCode, string defaultLang = "")
        {
            var itm = await Context.ExperienceLanguages
                .Where(e => e.ExperienceId == experienceId)
                .Where(e => e.LangCode == langCode.Trim().ToLower())
                .SingleOrDefaultAsync();

            if(itm == null)
            {
                if (!string.IsNullOrEmpty(defaultLang) && langCode != defaultLang)
                {
                    itm = await Context.ExperienceLanguages
                       .Where(e => e.ExperienceId == experienceId)
                       .Where(e => e.LangCode == defaultLang.Trim().ToLower())
                       .SingleOrDefaultAsync();
                }
                if (itm == null)
                {
                    itm = await Context.ExperienceLanguages
                       .Where(e => e.ExperienceId == experienceId)
                       .FirstOrDefaultAsync();
                }
            }
            return itm;
        }
        
        public async Task<IList<EExperienceLanguage>> GetList(Guid experienceId)
        {
            return await Context.ExperienceLanguages
                .Where(e => e.ExperienceId == experienceId)
                .ToListAsync();
        }

        private async Task<bool> Exists(EExperienceLanguage entry)
        {
            var itm = Context.ExperienceLanguages.Where(e => e.Id == entry.Id && e.LangCode == entry.LangCode);
            return await itm.AnyAsync();
        }
    }
}
