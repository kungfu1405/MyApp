using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class AttractionFilters
    {
        public AttractionFilters()
        {
        }
        public string Name { get; set; }
        public Guid? DestinationId { get; set; }

        public int? AttractionTypes { get; set; }
        public int? TouristClasses { get; set; }
        public int? TouristObjects { get; set; }
        public int? PropertyGroups { get; set; }

        public string Tags { get; set; }

        public string LangCode { get; set; }
        public string DefaultLang { get; set; }

        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
