using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Vehicle")]
    public class EVehicle : EntityBase<EVehicle>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CssIcon { get; set; }

        public int Ordinal { get; set; }
    }
}
