using DbData.Entities;
using Mic.Core.Entities;
using System.Collections.Generic;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class CountryViewModel : BaseModel
    {
        public CountryViewModel()
        {
            ActionMode = FormActionMode.Add;
            Country = new ECountry();
            Continents = new List<EContinent>();
        }
        
        public ECountry Country { get; set; }
        public IList<EContinent> Continents { get; set; }
    }

    public class CountryListViewModel : BaseModel
    {
        public CountryListViewModel()
        {
            Continents = new List<EContinent>();
        }
        public string Name { get; set; }
        public int? ContinentId { get; set; }
        public IList<EContinent> Continents { get; set; }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
