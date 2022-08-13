using DbData.Entities;
using Mic.Core.Entities;
using System.Collections.Generic;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class StateViewModel : BaseModel
    {
        public StateViewModel()
        {
            ActionMode = FormActionMode.Add;
            State = new EState { CountryCode = "na" };
            Countries = new List<ECountry>();
        }

        public EState State { get; set; }
        public IList<ECountry> Countries { get; set; }
    }

    public class StateListViewModel : BaseModel
    {
        public StateListViewModel()
        {
            Countries = new List<ECountry>();
        }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public IList<ECountry> Countries { get; set; }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
