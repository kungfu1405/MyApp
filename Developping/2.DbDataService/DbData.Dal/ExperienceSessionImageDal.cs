using DbData.Dal.Interfaces;
using DbData.Entities;
using Mic.Core.Dal;
using DbData.Entities.Models;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class ExperienceSessionImageDal : DalBase<IDbDataContext, EExperienceSessionImage>, IExperienceSessionImage
    {
        public ExperienceSessionImageDal(IDbDataContext context) : base(context)
        {
        }

        public async Task DeleteBy(Guid sessionId)
        {
            var images = Context.ExperienceSessionImages.Where(e => e.ExperienceSessionId == sessionId);
            if (await images.AnyAsync())
            {
                Context.ExperienceSessionImages.RemoveRange(images);
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll(Guid experienceId)
        {
            var images = Context.ExperienceSessionImages.Where(e => e.ExperienceId == experienceId);
            if (await images.AnyAsync())
            {
                Context.ExperienceSessionImages.RemoveRange(images);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<IList<EExperienceSessionImage>> GetList(Guid experienceId, Guid experienceSessionId)
        {
            return await Context.ExperienceSessionImages
                .Where(e => e.ExperienceSessionId == experienceSessionId)
                .Where(e => e.ExperienceId == experienceId)
                .OrderBy(e => e.Ordinal).ThenBy(e => e.CreateDate)
                .ToListAsync();
        }
    }
}
