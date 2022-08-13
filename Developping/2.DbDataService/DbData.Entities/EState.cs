using Mic.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("State")]
    public class EState : EntityBase<EState>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        [Required]
        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string FipsCode { get; set; }
        [StringLength(50)]
        public string Iso2 { get; set; }
        [StringLength(50)]
        public string WikiDataId { get; set; }

        public virtual ECountry Country { get; set; }
    }
}
