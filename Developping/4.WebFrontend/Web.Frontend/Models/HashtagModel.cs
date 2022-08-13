using DbData.Entities;
using DbData.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class HashtagModel
    {
        public HashtagModel()
        {
            ListExperience = new List<EExperience>();
            ListAttraction = new List<EAttraction>();
            TypeSearch = EnumTypeSearch.Story;
            TotalRecords = 0;
            Keyword = string.Empty;
            Page = 1;
            Items = 6;
        }
        public string Hashtag { get; set;}
        public IList<EExperience> ListExperience { get; set; }

        public IList<EAttraction> ListAttraction { get; set; }

        public EnumTypeSearch TypeSearch { get; set; }

        public int Page { get; set; }

        public int Items { get; set; }

        public int TotalRecords { get; set; }

        public string Keyword { get; set; }
    }
}
