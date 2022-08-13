using Mic.Core.Dal;
using DbData.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbData.Dal
{
    public interface IDbDataContext : IEfContext
    {
        DbSet<EContinent> Continents { get; set; }
        DbSet<ECountry> Countries { get; set; }
        DbSet<EState> States { get; set; }
        DbSet<ECity> Cities { get; set; }

        DbSet<ETag> Tags { get; set; }
        DbSet<EVehicle> Vehicles { get; set; }
        DbSet<EAttProperty> AttProperties { get; set; }

        DbSet<EDestination> Destinations { get; set; }
        DbSet<EDestinationLanguage> DestinationLanguages { get; set; }
        DbSet<EDestinationOverview> DestinationOverviews { get; set; }
        DbSet<EDestinationLink> DestinationLinks { get; set; }
        DbSet<EDestinationTag> DestinationTags { get; set; }

        DbSet<EAttraction> Attractions { get; set; }
        DbSet<EAttractionLanguage> AttractionLanguages { get; set; }
        DbSet<EAttractionLink> AttractionLinks { get; set; }
        DbSet<EAttractionProperty> AttractionProperties { get; set; }
        DbSet<EAttractionTag> AttractionTags { get; set; }

        DbSet<EExperience> Experiences { get; set; }
        DbSet<EExperienceLanguage> ExperienceLanguages { get; set; }
        DbSet<EExperienceSession> ExperienceSessions { get; set; }
        DbSet<EExperienceSessionImage> ExperienceSessionImages { get; set; }
        DbSet<EExperienceAttractionRef> ExperienceAttractionRefs { get; set; }
        DbSet<EExperienceTag> ExperienceTags { get; set; }

        DbSet<EComment> Comments { get; set; }
        DbSet<ECommentImage> CommentImages { get; set; }
        DbSet<EComment_LikeLog> Comment_LikeLogs { get; set; }

        DbSet<EUserProfile> UserProfiles { get; set; }
        DbSet<EUserFollow> UserFollows { get; set; }
        DbSet<EItemView> ItemViews { get; set; }
    }
}
