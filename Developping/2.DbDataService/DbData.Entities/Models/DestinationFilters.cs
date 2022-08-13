using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class DestinationFilters
    {
        public DestinationFilters()
        {
        }

        public string Name { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public int? CountryId { get; set; }
        public string State { get; set; }
        public int? StateId { get; set; }
        public string City { get; set; }
        public int? CityId { get; set; }
        public string Tags { get; set; }

        public string LangCode { get; set; }
        public string DefaultLang { get; set; }

        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
