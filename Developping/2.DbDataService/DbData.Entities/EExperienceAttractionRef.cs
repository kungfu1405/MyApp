using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("ExperienceAttractionRef")]
    public class EExperienceAttractionRef
    {
        public Guid Id { get; set; }
        public Guid ExperienceId { get; set; }
        public Guid AttractionId { get; set; }

        public virtual EExperience Experience { get; set; }
        public virtual EAttraction Attraction { get; set; }
    }
}
