using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbData.Entities;
using Mic.Core.Entities;

namespace Web.Frontend.Models
{
    public class DestinationModel
    {
        public DestinationModel()
        {
            Pagination = new KTPagination();
            Sort = new KTSort();
            _ListExperience = new ListExperience();
            _ListAllAttraction = new ListAttraction();
        }
        public EDestination DestinationDetail { get; set; }
    
        public ListAttraction _ListAllAttraction { get; set; }
   
        public ListExperience _ListExperience { get; set; }
        public int TypeOfAtt { get; set; }
        public string RouteUri { get; set; }
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
    public class ListExperience
    {
        public int TotalRecord { get; set; }
        public IList<EExperience> listExperience { get; set; }
    }
    public class ListAttraction
    {
        public int TotalRecord { get; set; }
        public IList<EAttraction> listAttraction { get; set; }
    }
}
