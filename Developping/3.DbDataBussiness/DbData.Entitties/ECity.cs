using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entitties
{
    [Table("City")]
    public class ECity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Longitude { get; set; }


        //public int StateId { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string StateCode { get; set; }

        //public int CountryId { get; set; }

        //[Required]
        //[StringLength(2)]
        //[Column(TypeName = "char(2)")]
        //public string CountryCode { get; set; }

        //[Column(TypeName = "decimal(18,6)")]
        //public decimal Latitude { get; set; }
        //[Column(TypeName = "decimal(18,6)")]
        //public decimal Longitude { get; set; }

        //[StringLength(50)]
        //public string WikiDataId { get; set; }

        //public virtual EState State { get; set; }

        //public virtual ECountry Country { get; set; }
    }
}
