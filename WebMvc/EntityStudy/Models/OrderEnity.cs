using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityStudy.Models
{
    [Table("OrderTbl")]
    public class OrderEnity
    {
        public int Id { get; set; }
        //   public int CustomerID { get; set; }
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public virtual ProductEntity Product { get; set; }
        //public ICollection<ProductOrdersEntity> ProductOrders { get; set; }
    }
}
