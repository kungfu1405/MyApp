using Mic.Core.Entities;
using System;

namespace DbData.Entities.Models
{
    public class TagFilters
    {
        public TagFilters()
        {

        }
        public Guid? ExperienceId { get; set; }
        public string Name { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
