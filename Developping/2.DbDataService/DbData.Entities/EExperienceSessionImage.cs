using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("ExperienceSessionImage")]
    public class EExperienceSessionImage
    {
        public Guid Id { get; set; }
        public Guid ExperienceId { get; set; }
        public Guid ExperienceSessionId { get; set; }

        [StringLength(200)]
        public string ImagerUrl { get; set; }

        public DateTime CreateDate { get; set; }
        public int Ordinal { get; set; }
    }
}
