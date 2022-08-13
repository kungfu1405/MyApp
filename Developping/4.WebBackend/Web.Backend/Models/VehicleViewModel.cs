using DbData.Entities;
using Mic.Core.Entities;
using Mic.Core.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{

    public class VehicleViewModel : BaseModel
    {
        public VehicleViewModel()
        {
            ActionMode = FormActionMode.Add;
            Vehicle = new EVehicle();
        }
        public EVehicle Vehicle { get; set; }
    }
    public class VehicleListViewModel : BaseModel
    {
        public VehicleListViewModel()
        {

        }
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
        public string Name { get; set; }                
    }
}
