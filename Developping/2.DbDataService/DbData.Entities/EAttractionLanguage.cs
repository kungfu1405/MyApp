using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("AttractionLanguage")]
    public class EAttractionLanguage
    {
        public Guid Id { get; set; }
        public Guid AttractionId { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LangCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual EAttraction Attraction { get; set; }
    }
}
