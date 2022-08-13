using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class AttPropertyFillter
    {
        public AttPropertyFillter()
        {

        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DatatablePaging  Paging { get; set; }
        public DatatableSort Sort { get; set; }    
    }
}
