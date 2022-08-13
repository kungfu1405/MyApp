using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("AttProperty")]
    public class EAttProperty : EntityBase<EAttProperty>
    {
        public Guid Id { get; set; }
        public EnumPropertyGroup PropertyGroup { get; set; }        
        public EnumDataType DataType { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string CssIcon { get; set; }
        public int Ordinal { get; set; }
    }
}
