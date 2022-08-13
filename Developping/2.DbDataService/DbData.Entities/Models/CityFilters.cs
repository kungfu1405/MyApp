using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class CityFilters
    {
        public CityFilters()
        {

        }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
