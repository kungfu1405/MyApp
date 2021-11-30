using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class OrderEnity
    {
        public int Id { get; set; }        
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public ICollection<ProductOrdersEntity> Products { get; set; }
    }
}
