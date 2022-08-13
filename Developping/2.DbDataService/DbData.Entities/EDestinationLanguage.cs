using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("DestinationLanguage")]
    public class EDestinationLanguage
    {
        public Guid Id { get; set; }
        public Guid DestinationId { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string LangCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual EDestination Destination { get; set; }
    }
}
