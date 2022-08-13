using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class VehicleFilter
    {
        public VehicleFilter()
        {

        }
        public string Name { get; set; }
        
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
