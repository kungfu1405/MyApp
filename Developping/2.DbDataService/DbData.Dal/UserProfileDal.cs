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
using Mic.UserDb.Entities;
namespace DbData.Dal
{
    public class UserProfileDal : DalBase<IDbDataContext, EUserProfile>, IUserProfile
    {
        public UserProfileDal(IDbDataContext context) : base(context)
        {
        }

        public override async Task<EUserProfile> Add(EUserProfile entry)
        {
            entry.Id = Guid.NewGuid();

            return await base.Add(entry);
        }
        public override async Task Edit(EUserProfile entry)
        {
            var itm = await Context.UserProfiles.SingleOrDefaultAsync(u => u.Id == entry.Id);
            if (itm == null)
            {
                await base.Add(entry);
                return;
            }

            itm.BannerUrl = entry.BannerUrl;
            itm.Intro = entry.Intro;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateCount(EnumStatisticType statisticType, Guid id, int number)
        {
            if (number == 0)
                return;

            var itm = await Context.UserProfiles.SingleOrDefaultAsync(u => u.Id == id);
            if (itm == null)
                return;

            switch (statisticType)
            {
                case EnumStatisticType.Experience:
                    if (number > 0)
                        itm.TotalExperiences += 1;
                    else
                        itm.TotalExperiences -= 1;
                    break;
                case EnumStatisticType.Plan:
                    if (number > 0)
                        itm.TotalPlans += 1;
                    else
                        itm.TotalPlans -= 1;
                    break;
                case EnumStatisticType.Comment:
                    if (number > 0)
                        itm.TotalComments += 1;
                    else
                        itm.TotalComments -= 1;
                    break;
                case EnumStatisticType.Rate:
                    itm.AvgRates = ((itm.AvgRates * itm.TotalRates) + number) / (itm.TotalRates + 1);
                    itm.TotalRates += 1;
                    break;
                case EnumStatisticType.Follower:
                    if (number > 0)
                        itm.TotalFollowers += 1;
                    else
                        itm.TotalFollowers -= 1;
                    break;
                case EnumStatisticType.Following:
                    if (number > 0)
                        itm.TotalFollowings += 1;
                    else
                        itm.TotalFollowings -= 1;
                    break;
            }
            await Context.SaveChangesAsync();
            
        }
    }
}
