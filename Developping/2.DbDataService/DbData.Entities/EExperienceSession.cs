using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("ExperienceSession")]
    public class EExperienceSession : EntityBase<EExperienceSession>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LangCode { get; set; }

        public Guid ExperienceId { get; set; }
        public Guid? AttractionId { get; set; }
        public Guid? DefaultGalleryId { get; set; }
        public int ImageDisplayType { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(150)]
        public string SubTitle { get; set; }
        public string Detail { get; set; }

        public string TranslateBy { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Ordinal { get; set; }
        public EnumPostStatus Status { get; set; }
        public string StatusName
        {
            get
            {
                return Status.ToString();
            }
        }

        public virtual EExperience Experience { get; set; }
        public virtual EAttraction Attraction { get; set; }

        [NotMapped]
        public IList<EExperienceSessionImage> Images { get; set; }
    }
}
