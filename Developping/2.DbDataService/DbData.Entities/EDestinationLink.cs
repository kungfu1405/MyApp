using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("DestinationLink")]
    public class EDestinationLink
    {
        public Guid Id { get; set; }
        public Guid FromDestinationId { get; set; }
        public Guid ToDestinationId { get; set; }
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

        public virtual EDestination FromDestination { get; set; }
        public virtual EDestination ToDestination { get; set; }
        public virtual EVehicle Vehicle { get; set; }
    }
}
