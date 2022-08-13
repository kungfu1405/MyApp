using Mic.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Continent")]
    public class EContinent : EntityBase<EContinent>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
