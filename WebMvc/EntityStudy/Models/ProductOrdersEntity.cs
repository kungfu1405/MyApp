using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityStudy.Models
{
    //    [Keyless]
    [Table("ProductOrderTbl")]
    public class ProductOrdersEntity
    {
        public int Id { get; set; }
        //[Key, Column(Order = 0)]
        public int ProductID { get; set; }
        //[Key, Column(Order = 1)]
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public virtual ProductEntity Product { get; set; }
        public virtual OrderEnity Order { get; set; }
    }
}
