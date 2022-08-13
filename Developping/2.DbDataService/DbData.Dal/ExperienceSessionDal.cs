using DbData.Dal.Interfaces;
using DbData.Entities;
using Mic.Core.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mic.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace DbData.Dal
{
    public class ExperienceSessionDal : DalBase<IDbDataContext, EExperienceSession>, IExperienceSession
    {
        public ExperienceSessionDal(IDbDataContext context) : base(context)
        {
        }

        public async override Task<EExperienceSession> Add(EExperienceSession entry)
        {
            if (new Guid().Equals(entry.Id))
                entry.Id = Guid.NewGuid();
            else if (await Exists(entry))
                throw new DuplicatedDataException($"Section's Language '{entry.LangCode}' Existed.");

            entry.CreateDate = DateTime.UtcNow;
            return await base.Add(entry);
        }

        public async override Task Edit(EExperienceSession entry)
        {
            try
            {
                var itm = await Context.ExperienceSessions.SingleOrDefaultAsync(u => u.Id == entry.Id);
                if (itm == null)
                    throw new DataNotFoundException("Section does not exists."
                        , new Exception($"SectionID ({entry.Id}) does not exists."));

                if (!string.IsNullOrWhiteSpace(entry.LangCode))
                    itm.LangCode = entry.LangCode;

                itm.ImageDisplayType = entry.ImageDisplayType;
                itm.DefaultGalleryId = entry.DefaultGalleryId;

                if(entry.AttractionId != null && entry.AttractionId != Guid.Empty)
                    itm.AttractionId = entry.AttractionId;

                itm.Title = entry.Title;
                itm.SubTitle = entry.SubTitle;
                itm.Detail = entry.Detail;
                itm.Ordinal = entry.Ordinal;
                itm.Status = entry.Status;
                await Context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Edit Section Error: {ex.Message}");
            }
        }

        public async override Task Delete(object id)
        {
            var itms = await Context.ExperienceSessions
                .Where(e => e.Id == (Guid)id)
                .ToListAsync();

            if (itms.Any())
            {
                var images = Context.ExperienceSessionImages
                    .Where(e => e.ExperienceSessionId == itms.First().Id);

                Context.ExperienceSessionImages.RemoveRange(images);
                Context.ExperienceSessions.RemoveRange(itms);
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteBy(Guid experienceId)
        {
            var itms = Context.ExperienceSessions.Where(e => e.ExperienceId == experienceId);
            if (await itms.AnyAsync())
            {
                Context.ExperienceSessions.RemoveRange(itms);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<EExperienceSession> Get(Guid id, string langCode, string defaultLang = "")
        {
            var itm = await Context.ExperienceSessions.SingleOrDefaultAsync(e => e.Id == (Guid)id && e.LangCode == langCode);
            if(itm == null)
            {
                if(!string.IsNullOrEmpty(defaultLang))
                    itm = await Context.ExperienceSessions.SingleOrDefaultAsync(e => e.Id == (Guid)id && e.LangCode == defaultLang);
                if (itm == null)
                    itm = await Context.ExperienceSessions.FirstOrDefaultAsync(e => e.Id == (Guid)id);
            }    
            if (itm != null)
            {
                itm.Images = await Context.ExperienceSessionImages
                    .Where(e => e.ExperienceSessionId == itm.Id)
                    .OrderBy(e => e.Ordinal)
                    .ThenBy(e => e.CreateDate)
                    .ToListAsync();

                if (itm.AttractionId.HasValue)
                {
                    itm.Attraction = await Context.Attractions.FirstOrDefaultAsync(e => e.Id == itm.AttractionId.Value);
                }
            }
            return itm;
        }

        public async Task<IList<EExperienceSession>> GetList(Guid? experienceId = null, Guid? sessionId = null, string langCode = "", string defaultLang = "")
        {
            var result = Context.ExperienceSessions
                .Where(e => !experienceId.HasValue || e.ExperienceId == experienceId)
                .Where(e => !sessionId.HasValue || e.Id == sessionId);

            if (!string.IsNullOrWhiteSpace(langCode))
            {
                result = result.Where(e => e.LangCode == langCode.Trim().ToLower());
            }
            else if (!string.IsNullOrWhiteSpace(defaultLang))
            {
                result = result.Where(e => e.LangCode == defaultLang.Trim().ToLower());
            }

            if (!await result.AnyAsync())
            {
                var firstSession = await Context.ExperienceSessions
                    .FirstOrDefaultAsync(e => e.ExperienceId == experienceId);

                if (firstSession != null)
                {
                    result = Context.ExperienceSessions
                        .Where(e => e.ExperienceId == experienceId)
                        .Where(e => e.LangCode == firstSession.LangCode);
                }
                else
                    return new List<EExperienceSession>();
            }

            return await result
                .OrderBy(e=>e.Ordinal)
                .ThenBy(e => e.CreateDate)
                .ToListAsync();
        }

        public async Task<IList<EExperienceSession>> Search(string q, string langCode = "", string defaultLang = "")
        {
            var result = Context.ExperienceSessions.Where(e => e.Title.Contains(q));

            if (!string.IsNullOrWhiteSpace(langCode))
            {
                result = result.Where(e => e.LangCode == langCode.Trim().ToLower());
            }
            else if (!string.IsNullOrWhiteSpace(defaultLang))
            {
                result = result.Where(e => e.LangCode == defaultLang.Trim().ToLower());
            }

            return await result
               .OrderBy(e => e.Ordinal)
               .ThenBy(e => e.CreateDate)
               .ToListAsync();
        }

        private async Task<bool> Exists(EExperienceSession entry)
        {
            var itm = Context.ExperienceSessions.Where(e => e.Id == entry.Id && e.LangCode == entry.LangCode);
            return await itm.AnyAsync();
        }

    }
}
