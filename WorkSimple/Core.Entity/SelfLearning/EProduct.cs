using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.SelfLearning
{
    [Table("tblProduct")]
    public class EProduct
    {
        public int Id { get; set; }      
        public double Price { get; set; }
        [Display(Name = "Description :")]
        [StringLength(200)]
        public string Description { get; set; }
        public int Quantity { get; set; }

        public int CateId { get; set; }
        public virtual ECategory Category { get; set; }
    }
}
