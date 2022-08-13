using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbData.Entities;
using Mic.Core.Entities;

namespace Web.Frontend.Models
{
    public class AttractionModel: DestinationModel
    {
        public EAttraction AttractionDetail { get; set; }
        public AttractionModel()
        {
            Pagination = new KTPagination();
            Sort = new KTSort();
            _ListExperience = new ListExperience();
            _ListAllAttraction = new ListAttraction();
            DestinationModel destinationModel = new DestinationModel();
        }
    }
}
