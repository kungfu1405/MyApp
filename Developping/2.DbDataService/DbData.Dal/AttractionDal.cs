using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class AttractionDal : DalBase<IDbDataContext, EAttraction>, IAttraction
    {
        public AttractionDal(IDbDataContext context) : base(context)
        {
        }
        public override async Task<EAttraction> Add(EAttraction entry)
        {
            //entry.Id = Guid.NewGuid();
            AddMulti(entry);
            return await Get(entry.Id);

        }
        public void AddMulti(EAttraction entry)
        {
            Context.Attractions.Add(entry);         
            if(entry.Tags.Count >0)
            {
                foreach (var item in entry.Tags)
                {
                    Context.Tags.Add(item);
                    EAttractionTag attTag = new EAttractionTag();
                    attTag.AttractionId = entry.Id;
                    attTag.TagId = item.Id;
                    Context.AttractionTags.Add(attTag);
                }
            }                        
            Context.SaveChangesAsync();
        }
        public async Task Edit(EAttraction entry)
        {
            var itm = await Context.Attractions.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
                throw new DataNotFoundException("Attraction does not exists."
                    , new Exception($"Attraction ({entry.Id}) does not exists."));
            itm.DefaultName = entry.DefaultName;
            itm.Address = entry.Address;
            itm.Description = entry.Description;
            itm.ThumbnailUrl = entry.ThumbnailUrl;
            itm.Address = entry.Address;
            itm.Contact = entry.Contact;
            itm.Website = entry.Website;
            itm.PriceFrom = entry.PriceFrom;
            itm.Latitude = entry.Latitude;
            itm.Longitude = entry.Longitude;
            itm.DestinationId = entry.DestinationId;
            itm.AttractionTypes = entry.AttractionTypes;
            itm.TouristClasses = entry.TouristClasses;
            itm.TouristObjects = entry.TouristObjects;
            itm.PropertyGroups = entry.PropertyGroups;
            
            if (entry.Tags.Count > 0)
            {
                List<EAttractionTag> ListAttTagCheck = new List<EAttractionTag>();
                foreach (var item in entry.Tags)
                {
                    ETag tagCheck = Context.Tags.SingleOrDefault(e => e.Name == item.Name);
                    //EAttractionTag = Context.AttractionTags.SingleOrDefault(e => e.AttractionId == item.Id)
                    EAttractionTag attTag = new EAttractionTag();
                    if (tagCheck == null)
                    {
                        Context.Tags.Add(item);
                        attTag.TagId = item.Id;
                        attTag.AttractionId = entry.Id;
                        Context.AttractionTags.Add(attTag);
                    }
                    else
                    {
                        attTag.TagId = tagCheck.Id;
                        EAttractionTag attagCheck = Context.AttractionTags.SingleOrDefault(e => e.AttractionId == entry.Id && e.TagId == attTag.TagId);
                        if(attagCheck == null)
                        {
                            attTag.AttractionId = entry.Id;
                            Context.AttractionTags.Add(attTag);
                        }    
                    }
                    ListAttTagCheck.Add(attTag);
                }
                // delete current item
                var atTagCurrents = Context.AttractionTags.AsQueryable().Where(e => e.AttractionId == entry.Id).ToList();
                foreach (var item in atTagCurrents)
                {
                    item.AttractionId = entry.Id;
                    var checkItem = ListAttTagCheck.SingleOrDefault(e =>e.TagId == item.TagId);
                    if (checkItem == null)
                    {
                        Context.AttractionTags.Remove(item);
                    }
                }
            }
            await Context.SaveChangesAsync();
        }
        public override async Task<EAttraction> Get(object id)
        {
            return await Context.Attractions.Include(e => e.Destination)
                .Where(e => e.Id == (Guid)id)
                .Include(e => e.AttractionLanguages)
                .Include(e => e.AttractionTags)
                .SingleOrDefaultAsync();
        }

        public async Task<EAttraction> Get(string routeUri)
        {
            var itm = await Context.Attractions.Include(e =>e.Destination)
                .SingleOrDefaultAsync(e => e.RouteUri == routeUri.Trim().ToLower());

            if (itm != null)
            {
                itm.Tags = await Context.AttractionTags
                    .Where(e => e.AttractionId == itm.Id)
                    .Select(e => e.Tag)
                    .ToListAsync();
            }
            return itm;
        }

        public async Task<PagingResult<EAttraction>> GetList(AttractionFilters filter = null)
        {
            var lst = Context.Attractions.Include(e=>e.Destination).AsQueryable();            
            if (filter != null)
            {
                lst = lst
                    .Where(e => !filter.DestinationId.HasValue || e.DestinationId == filter.DestinationId)
                    .Where(e => !filter.AttractionTypes.HasValue || filter.AttractionTypes == 0 || (e.AttractionTypes & filter.AttractionTypes) > 0)
                    .Where(e => !filter.TouristClasses.HasValue || filter.TouristClasses == 0 || (e.TouristClasses & filter.TouristClasses) > 0)
                    .Where(e => !filter.TouristObjects.HasValue || filter.TouristObjects == 0 || (e.TouristObjects & filter.TouristObjects) > 0)
                    .Where(e => !filter.PropertyGroups.HasValue || filter.PropertyGroups == 0 || (e.PropertyGroups & filter.PropertyGroups) > 0)
                    .Where(e => string.IsNullOrEmpty(filter.Name)
                        || e.DefaultName.Contains(filter.Name)
                        || e.AttractionLanguages.Any(l => l.Name.Contains(filter.Name)));

                if (!string.IsNullOrWhiteSpace(filter.Tags))
                {
                    var tagNames = filter.Tags.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var tagIds = await Context.Tags.Where(e => tagNames.Contains(e.Name)).Select(e => e.Id).ToListAsync();

                    lst = lst.Where(e => e.AttractionTags.Any(r => tagIds.Contains(r.TagId)));
                }
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderByDescending(e => e.AvgRates);

            var result = new PagingResult<EAttraction>();
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
    }
}
