using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class ExperienceDal : DalBase<IDbDataContext, EExperience>, IExperience
    {
        public ExperienceDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<EExperience> Add(EExperience entry)
        {
            entry.Id = Guid.NewGuid();
            entry.CreateDate = DateTime.UtcNow;
            entry.RouteUri = await getUnitedRoute(entry.RouteUri);

            return await base.Add(entry);
        }

        public override async Task Edit(EExperience entry)
        {
            var itm = await Context.Experiences.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Experience does not exists."
                    , new Exception($"Experience ({entry.Id}) does not exists."));

            if(itm.RouteUri != entry.RouteUri)
            {
                itm.RouteUri = await getUnitedRoute(entry.RouteUri);
            }
            itm.DefaultName = entry.DefaultName;
            itm.Description = entry.Description;
            itm.DestinationId = entry.DestinationId;
            itm.ThumbnailUrl = entry.ThumbnailUrl;
            itm.RefPlanId = entry.RefPlanId;

            itm.FromDate = entry.FromDate;
            itm.ToDate = entry.ToDate;
            itm.PublishDate = entry.PublishDate;
            itm.Status = entry.Status;

            itm.ExperienceLanguages = entry.ExperienceLanguages;
            //itm.ExperienceTags = entry.ExperienceTags;
            await Context.SaveChangesAsync();
        }

        public async Task AddAttraction(EExperienceAttractionRef entry)
        {
            var itm = await Context.Experiences
                .SingleOrDefaultAsync(e => e.Id == entry.ExperienceId);
            if (itm == null)
                throw new DataNotFoundException($"Record does not exists.");

            entry.Id = Guid.NewGuid();
            Context.ExperienceAttractionRefs.Add(entry);
            await Context.SaveChangesAsync();
        }

        public override async Task<EExperience> Get(object id)
        {
            return await Context.Experiences
                .Where(e => e.Id == (Guid)id)
                .Include(e => e.ExperienceTags)
                .Include(e => e.ExperienceLanguages)
                .Include(e => e.ExperienceAttractionRefs)
                .SingleOrDefaultAsync();
        }

        public async Task<EExperience> Get(string routeUri)
        {
            var itm = await Context.Experiences
                .SingleOrDefaultAsync(e => e.RouteUri == routeUri.Trim().ToLower());

            if (itm != null)
            {
                itm.Tags = await Context.ExperienceTags
                    .Where(e => e.ExperienceId == itm.Id)
                    .Select(e => e.Tag)
                    .ToListAsync();
            }
            return itm;
        }

        public async Task<PagingResult<EExperience>> GetList(ExperienceFilters filter = null)
        {
            var lst = Context.Experiences.AsQueryable();

            if (filter != null)
            {
                lst = lst
                    .Where(e => !filter.DestinationId.HasValue || e.DestinationId == filter.DestinationId)
                    .Where(e => !filter.AuthorId.HasValue || e.AuthorId == filter.AuthorId)
                    .Where(e => !filter.Status.HasValue || e.Status == (EnumPostStatus)filter.Status)
                    .Where(e => !filter.FromDate.HasValue || e.PublishDate >= filter.FromDate)
                    .Where(e => !filter.ToDate.HasValue || e.PublishDate <= filter.ToDate)
                    .Where(e => !filter.AttractionId.HasValue || e.ExperienceAttractionRefs.Any(r => r.AttractionId == filter.AttractionId))
                    .Where(e => string.IsNullOrEmpty(filter.Title)
                        || e.ExperienceLanguages.Any(l => l.Title.Contains(filter.Title)));

                if (!string.IsNullOrWhiteSpace(filter.Tags))
                {
                    var tagNames = filter.Tags.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var tagIds = await Context.Tags.Where(e => tagNames.Contains(e.Name)).Select(e => e.Id).ToListAsync();

                    lst = lst.Where(e => e.ExperienceTags.Any(r => tagIds.Contains(r.TagId)));
                }
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderByDescending(e => e.AvgRates);

            var result = new PagingResult<EExperience>();
            result.TotalRecords = await lst.CountAsync();

            //Paging
            if (filter == null)
            {
                filter.Paging = new DatatablePaging
                {
                    Start = 0,
                    Length = 30
                };
            }
            result.Data = await DalUtils.Paging(lst, filter.Paging).ToListAsync();
            return result;
        }

        private async Task<string> getUnitedRoute(string route)
        {
            var routeUri = route;
            var count = 0;
            while (true)
            {
                if (await Context.Experiences.AnyAsync(e => route.Equals(e.RouteUri)))
                {
                    route = routeUri + "-" + count;
                    count++;
                }
                else
                    break;
            }
            return route.Replace("?", "").Replace("&", "").Replace("#", "");
        }

        public async Task AddExperienceTag(EExperienceTag entry)
        {
            if (entry.ExperienceId == Guid.Empty || entry.TagId == Guid.Empty)
                return;

            var itm = await Context.ExperienceTags.SingleOrDefaultAsync(e => e.ExperienceId == entry.ExperienceId && e.TagId == entry.TagId);
            if (itm != null)
            {
                return;
            }

            Context.ExperienceTags.AddRange(entry);
            await Context.SaveChangesAsync();
        }
    }
}
