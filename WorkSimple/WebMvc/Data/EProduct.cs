using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Data
{

    public class EProduct
    {
        public int Id { get; set; }
        public int CateId { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
