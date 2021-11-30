using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityStudy.Models
{
    [Table("ProductTbl")]
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CateId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}
