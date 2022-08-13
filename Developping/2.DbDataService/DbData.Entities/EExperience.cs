using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Experience")]
    public class EExperience : EntityBase<EExperience>
    {
        public Guid Id { get; set; }
        public Guid? DestinationId { get; set; }

        [StringLength(200)]
        public string RouteUri { get; set; }

        [StringLength(200)]
        public string DefaultName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string ThumbnailUrl { get; set; }

        public Guid? RefPlanId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [StringLength(50)]
        public string Author { get; set; }
        public Guid AuthorId { get; set; }

        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int TotalRates { get; set; }
        public double AvgRates { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime PublishDate { get; set; }
        public EnumPostStatus Status { get; set; }

        public virtual EDestination Destination { get; set; }
        public virtual ICollection<EExperienceTag> ExperienceTags { get; set; }

        [NotMapped]
        public EExperienceLanguage ExperienceLanguage { get; set; }
        public virtual IList<EExperienceLanguage> ExperienceLanguages { get; set; }

        public virtual ICollection<EExperienceSession> ExperienceSessions { get; set; }
        public virtual ICollection<EExperienceAttractionRef> ExperienceAttractionRefs { get; set; }


        [NotMapped]
        public IList<ETag> Tags { get; set; }

    }
}
