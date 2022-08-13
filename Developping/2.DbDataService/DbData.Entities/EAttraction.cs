using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Attraction")]
    public class EAttraction : EntityBase<EAttraction>
    {
        public Guid Id { get; set; }
        public Guid DestinationId { get; set; }

        //[Required]
        [StringLength(200)]
        public string RouteUri { get; set; }
        
        [StringLength(200)]
        public string DefaultName { get; set; }

        [StringLength(500)]
       
        public string Description { get; set; }

        // EnumAttractionType        

        public int AttractionTypes { get; set; }

        // EnumTouristClass        

        public int TouristClasses { get; set; }

        // EnumTouristObject
      
        public int TouristObjects { get; set; }

        // EnumPropertyGroup

        public int? PropertyGroups { get; set; }

        [StringLength(200)]
        public string ThumbnailUrl { get; set; }
        public Guid? DefaultGalleryId { get; set; }
        public Guid? DefaultExperienceId { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Contact { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        public int? VisitDuration { get; set; }
        public double? PriceFrom { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        [Column(TypeName = "decimal(10, 8)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal? Longitude { get; set; }

        [StringLength(200)]
        public string MapImageUri { get; set; }

        public long TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public int TotalRates { get; set; }
        public double AvgRates { get; set; }

        [StringLength(50)]
        public string Author { get; set; }
        public Guid? AuthorId { get; set; }
        public DateTime CreateDate { get; set; }

        public bool FromExperience { get; set; }
        public EnumPriority Priority { get; set; }
        public EnumPostStatus Status { get; set; }
        
        public virtual EDestination Destination { get; set; }
        public virtual ICollection<EAttractionTag> AttractionTags { get; set; }

        public virtual IList<EAttractionLanguage> AttractionLanguages { get; set; }

        [NotMapped]
        public EAttractionLanguage AttractionLanguage { get; set; }

        [NotMapped]
        public IList<ETag> Tags { get; set; }
    }
}
