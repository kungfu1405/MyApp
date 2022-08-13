using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class StateFilters
    {
        public StateFilters()
        {

        }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
