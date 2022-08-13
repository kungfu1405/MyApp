using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("ExperienceTag")]
    public class EExperienceTag
    {
        public Guid ExperienceId { get; set; }
        public Guid TagId { get; set; }

        public virtual EExperience Experience { get; set; }
        public virtual ETag Tag { get; set; }
    }
}
