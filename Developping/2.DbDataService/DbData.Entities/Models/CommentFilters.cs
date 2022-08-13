using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class CommentFilters
    {
        public CommentFilters()
        {

        }
        public Guid? AttractionId { get; set; }
        public Guid? ExperienceId { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
