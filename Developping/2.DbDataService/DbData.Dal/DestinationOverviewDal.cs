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
    public class DestinationOverviewDal : DalBase<IDbDataContext, EDestinationOverview>, IDestinationOverview
    {
        public DestinationOverviewDal(IDbDataContext context) : base(context)
        {
        }

        public async Task<EDestinationOverview> Get(Guid destinationId, string langCode, string defaultLang = "")
        {
            var itm = await Context.DestinationOverviews
                .Where(e => e.Id == destinationId)
                .Where(e => e.LangCode == langCode.Trim().ToLower())
                .SingleOrDefaultAsync();

            if (itm == null)
            {
                if (!string.IsNullOrEmpty(defaultLang) && langCode != defaultLang)
                {
                    itm = await Context.DestinationOverviews
                       .Where(e => e.Id == destinationId)
                       .Where(e => e.LangCode == defaultLang.Trim().ToLower())
                       .SingleOrDefaultAsync();
                }
                if (itm == null)
                {
                    itm = await Context.DestinationOverviews
                       .Where(e => e.Id == destinationId)
                       .FirstOrDefaultAsync();
                }
            }
            return itm;
        }

        public async Task<IList<EDestinationOverview>> GetList(Guid destinationId)
        {
            return await Context.DestinationOverviews
                .Where(e => e.Id == destinationId)
                .ToListAsync();
        }
    }
}
