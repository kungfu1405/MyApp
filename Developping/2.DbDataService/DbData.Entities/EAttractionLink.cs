using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("AttractionLink")]
    public class EAttractionLink
    {
        public Guid Id { get; set; }
        public Guid FromAttractionId { get; set; }
        public Guid ToAttractionId { get; set; }
        public Guid? VehicleId { get; set; }

        public int? Distance { get; set; }
        public int? Duration { get; set; }

        public double? PriceFrom { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        public int LinkedCount { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }
        public EnumPriority Priority { get; set; }

        public virtual EAttraction FromAttraction { get; set; }
        public virtual EAttraction ToAttraction { get; set; }
        public virtual EVehicle Vehicle { get; set; }
    }
}
