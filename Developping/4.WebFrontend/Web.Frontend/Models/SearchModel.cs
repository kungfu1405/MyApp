using DbData.Entities;
using DbData.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class SearchModel
    {
        public SearchModel(){
            ListExperience = new List<EExperience>();
            ListAttraction = new List<EAttraction>();
            TypeSearch = EnumTypeSearch.Story;
            CountResult = 0;
            keyword = string.Empty;
            page = 1;
        }

        public IList<EExperience> ListExperience;

        public IList<EAttraction> ListAttraction;

        public EnumTypeSearch TypeSearch;

        public int CountResult;

        public string keyword;

        public int page;
    }
}
