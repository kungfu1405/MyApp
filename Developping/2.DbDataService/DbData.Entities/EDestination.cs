using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Destination")]
    public class EDestination : EntityBase<EDestination>
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Continent { get; set; }

        public int CountryId { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        public int? StateId { get; set; }

        [StringLength(100)]
        public string StateName { get; set; }

        public int? CityId { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string DefaultName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        //[Required]
        [StringLength(200)]
        public string RouteUri { get; set; }

        [StringLength(200)]
        public string ThumbnailUrl { get; set; }

        [StringLength(200)]
        public string BannerUrl { get; set; }

        public Guid? DestinationOverviewId { get; set; }
        public Guid? DefaultGalleryId { get; set; }

        [Column(TypeName = "decimal(10, 8)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(11, 8)")]
        public decimal? Longitude { get; set; }

        [StringLength(200)]
        public string MapImageUri { get; set; }

        public long TotalLikes { get; set; }
        public long TotalRates { get; set; }
        public double AvgRates { get; set; }
    
        public virtual ECountry Country { get; set; }
    
        public virtual EState State { get; set; }
     
        public virtual ECity City { get; set; }

        [NotMapped]
        public EDestinationLanguage DestinationLanguage { get; set; }
        public virtual IList<EDestinationLanguage> DestinationLanguages { get; set; }
        public virtual ICollection<EDestinationTag> DestinationTags { get; set; }

        [NotMapped]
        public IList<ETag> Tags { get; set; }

        [NotMapped]
        public EDestinationOverview DestinationOverview { get; set; }

        [NotMapped]
        public IList<EDestinationOverview> DestinationOverviews { get; set; }
    }
}
