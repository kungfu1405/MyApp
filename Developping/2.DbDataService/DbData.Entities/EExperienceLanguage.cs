using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("ExperienceLanguage")]
    public class EExperienceLanguage : EntityBase<EExperienceLanguage>
    {
        public Guid Id { get; set; }
        public Guid ExperienceId { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LangCode { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual EExperience Experience { get; set; }
    }
}
