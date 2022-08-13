using DbData.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DbData.Dal
{
    public class DbDataContext : DbContext, IDbDataContext
    {
        public DbDataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<EContinent>().HasKey(o => new { o.Id });
            builder.Entity<EContinent>().Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Entity<ECountry>().HasKey(o => new { o.Id });
            builder.Entity<ECountry>().Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Entity<EState>().HasKey(o => new { o.Id });
            builder.Entity<EState>().Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Entity<ECity>().HasKey(o => new { o.Id });
            builder.Entity<ECity>().Property(o => o.Id).ValueGeneratedOnAdd();

            builder.Entity<EDestinationTag>().HasKey(o => new { o.DestinationId, o.TagId });
            builder.Entity<EDestinationOverview>().HasKey(o => new { o.Id, o.LangCode });

            builder.Entity<EAttractionTag>().HasKey(o => new { o.AttractionId, o.TagId });
            builder.Entity<EExperienceTag>().HasKey(o => new { o.ExperienceId, o.TagId });
            builder.Entity<EExperienceSession>().HasKey(o => new { o.Id, o.LangCode });

            builder.Entity<EUserFollow>().HasKey(o => new { o.UserId, o.UserFollowingId });

            //builder.Ignore<ECity>().Entity<EDestination>();
            //builder.Ignore<EState>().Entity<EDestination>();
            //builder.Ignore<ECountry>().Entity<EDestination>();
            //builder.Ignore<EDestinationLanguage>().Entity<EDestination>().HasNoKey();

  
        }

        public DbSet<EContinent> Continents { get; set; }
        public DbSet<ECountry> Countries { get; set; }
        public DbSet<EState> States { get; set; }
        public DbSet<ECity> Cities { get; set; }

        public DbSet<ETag> Tags { get; set; }
        public DbSet<EVehicle> Vehicles { get; set; }
        public DbSet<EAttProperty> AttProperties { get; set; }

        public DbSet<EDestination> Destinations { get; set; }
        public DbSet<EDestinationLanguage> DestinationLanguages { get; set; }
        public DbSet<EDestinationOverview> DestinationOverviews { get; set; }
        public DbSet<EDestinationLink> DestinationLinks { get; set; }
        public DbSet<EDestinationTag> DestinationTags { get; set; }

        public DbSet<EAttraction> Attractions { get; set; }
        public DbSet<EAttractionLanguage> AttractionLanguages { get; set; }
        public DbSet<EAttractionLink> AttractionLinks { get; set; }
        public DbSet<EAttractionProperty> AttractionProperties { get; set; }
        public DbSet<EAttractionTag> AttractionTags { get; set; }

        public DbSet<EExperience> Experiences { get; set; }
        public DbSet<EExperienceLanguage> ExperienceLanguages { get; set; }
        public DbSet<EExperienceSession> ExperienceSessions { get; set; }
        public DbSet<EExperienceSessionImage> ExperienceSessionImages { get; set; }
        public DbSet<EExperienceAttractionRef> ExperienceAttractionRefs { get; set; }
        public DbSet<EExperienceTag> ExperienceTags { get; set; }

        public DbSet<EComment> Comments { get; set; }
        public DbSet<ECommentImage> CommentImages { get; set; }
        public DbSet<EComment_LikeLog> Comment_LikeLogs { get; set; }

        public DbSet<EUserProfile> UserProfiles { get; set; }
        public DbSet<EUserFollow> UserFollows { get; set; }

        public DbSet<EItemView> ItemViews { get; set; }
    }
}
