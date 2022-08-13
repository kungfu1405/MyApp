using Mic.Core.Entities;

namespace DbData.Entities.Models
{
    public class CountryFilters
    {
        public CountryFilters()
        {

        }
        public int? ContinentId { get; set; }
        public string Name { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
