using DbData.Dal.Interfaces;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.Dal;
using Mic.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Dal
{
    public class DestinationDal : DalBase<IDbDataContext, EDestination>, IDestination
    {
        public DestinationDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<EDestination> Add(EDestination entry)
        {
            entry.Id = Guid.NewGuid();

            return await base.Add(entry);
        }

        public async Task<EDestination> Get(string routeUri)
        {
            var itm = await Context.Destinations
                .SingleOrDefaultAsync(e => e.RouteUri == routeUri.Trim().ToLower());

            if (itm != null)
            {
                itm.Tags = await Context.DestinationTags
                    .Where(e => e.DestinationId == itm.Id)
                    .Select(e => e.Tag)
                    .ToListAsync();
            }
            return itm;
        }

        public async Task<PagingResult<EDestination>> GetList(DestinationFilters filter = null)
        {
            var lst = Context.Destinations.AsQueryable();
            if (filter != null)
            {
                lst = lst
                    .Where(e=> !filter.CountryId.HasValue || filter.CountryId == 0 || e.CountryId == filter.CountryId)
                    .Where(e=> !filter.StateId.HasValue || filter.StateId == 0 || e.StateId == filter.StateId)
                    .Where(e=> !filter.CityId.HasValue || filter.CityId == 0 || e.CityId == filter.CityId)
                    .Where(e=> string.IsNullOrEmpty(filter.Continent) || e.Continent == filter.Continent)
                    .Where(e=> string.IsNullOrEmpty(filter.Country) || e.CountryName == filter.Country)
                    .Where(e=> string.IsNullOrEmpty(filter.State) || e.StateName == filter.State)
                    .Where(e=> string.IsNullOrEmpty(filter.City) || e.CityName == filter.City)
                    .Where(e => string.IsNullOrEmpty(filter.Name)
                        || e.DefaultName.Contains(filter.Name)
                        || e.CountryName.Contains(filter.Name)
                        || e.StateName.Contains(filter.Name)
                        || e.CityName.Contains(filter.Name)
                        || e.DestinationLanguages.Any(l => l.Name.Contains(filter.Name)));

                if (!string.IsNullOrWhiteSpace(filter.Tags))
                {
                    var tagNames = filter.Tags.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    var tagIds = await Context.Tags.Where(e => tagNames.Contains(e.Name)).Select(e => e.Id).ToListAsync();

                    lst = lst.Where(e => e.DestinationTags.Any(r => tagIds.Contains(r.TagId)));
                }
            }

            if (filter != null && filter.Sort != null)
                lst = lst.OrderByDynamic(filter.Sort);
            else
                lst = lst.OrderByDescending(e => e.AvgRates);

            var result = new PagingResult<EDestination>();
            result.TotalRecords = await lst.CountAsync();
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
