using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("AttractionProperty")]
    public class EAttractionProperty
    {
        public Guid Id { get; set; }
        public Guid AttractionId { get; set; }
        public Guid PropertyId { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
        public int Ordinal { get; set; }

        public virtual EAttraction Attraction { get; set; }
        public virtual EAttProperty Property { get; set; }
    }
}
