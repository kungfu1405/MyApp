using DbData.Entities;
using Mic.Core.Entities;
using Mic.Core.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class AttPropertyViewModel:BaseModel
    {

        public AttPropertyViewModel()
        {
            ActionMode = FormActionMode.Add;
                  
        }
        public EAttProperty AttProperty {get; set; }
    }
    public class AttPropertyListViewModel:BaseModel
    {
        public AttPropertyListViewModel()
        {

        }        
        public string Name { get; set; }        
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }

    }
}
    