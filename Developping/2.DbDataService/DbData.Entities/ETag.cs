using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Tag")]
    public class ETag : EntityBase<ETag>
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
