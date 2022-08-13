using DbData.Entities;
using Mic.Core.Entities;
using System.Collections.Generic;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class CityViewModel : BaseModel
    {
        public CityViewModel()
        {
            ActionMode = FormActionMode.Add;
            City = new ECity { CountryCode = "na", StateCode = "na" };
        }

        public ECity City { get; set; }
    }

    public class CityListViewModel : BaseModel
    {
        public CityListViewModel()
        {
        }
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public int? StateyId { get; set; }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
