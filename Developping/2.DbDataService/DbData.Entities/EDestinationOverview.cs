using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("DestinationOverview")]
    public class EDestinationOverview : EntityBase<EDestinationOverview>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LangCode { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(150)]
        public string SubTitle { get; set; }

        [Required]
        [StringLength(200)]
        public string RouteUri { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Detail { get; set; }

        [StringLength(200)]
        public string ThumbnailUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        public Guid AuthorId { get; set; }

        public DateTime CreateDate { get; set; }
        public EnumPostStatus Status { get; set; }
    }
}
