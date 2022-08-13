using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class ExperienceFilters
    {
        public ExperienceFilters(){

        }
        public Guid? DestinationId { get; set; }
        public Guid? AttractionId { get; set; }
        public Guid? AuthorId { get; set; }

        public string Title { get; set; }
        public string Tags { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Status { get; set; }

        public string LangCode { get; set; }
        public string DefaultLang { get; set; }

        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
