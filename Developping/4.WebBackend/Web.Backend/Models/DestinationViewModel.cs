using DbData.Entities;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class DestinationViewModel : BaseModel
    {
        public DestinationViewModel()
        {
            ActionMode = FormActionMode.Add;
            Destination = new EDestination { 
                CountryName = "country name"
            };
            
        }
        public EDestination Destination { get; set; }
        public ECity City { get; set; }
        public EState State { get; set; }
        public EContinent Continent { get; set; }
        public ECountry Country { get; set; }
        public IList<ELanguage> Languages { get; set; }
    }
    public class DestinationListViewModel : BaseModel
    {
        public DestinationListViewModel()
        {
        }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }      
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string Tags { get; set; }
        public string langCode { get; set; }
        public string defaultLang { get; set; }
    }
}
