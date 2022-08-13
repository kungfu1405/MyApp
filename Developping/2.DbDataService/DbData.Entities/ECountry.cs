using Mic.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Country")]
    public class ECountry : EntityBase<ECountry>
    {
        [Key]
        public int Id { get; set; }
        public int? ContinentId { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string Iso2 { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char(3)")]
        public string Iso3 { get; set; }

        [StringLength(50)]
        public string PhoneCode { get; set; }

        [StringLength(50)]
        public string Capital { get; set; }

        [StringLength(50)]
        public string Currency { get; set; }

        [StringLength(50)]
        public string Native { get; set; }

        [StringLength(50)]
        public string Emoji { get; set; }

        [StringLength(50)]
        public string EmojiU { get; set; }

        [StringLength(50)]
        public string WikiDataId { get; set; }

        public virtual EContinent Continent { get; set; }
    }
}
