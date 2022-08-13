using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class PageRequestModel
    {
        public int page { get; set; }
        public string routeUri { get; set; }
        public int? typeOfAtt { get; set; }
    }
}
