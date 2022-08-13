using DbData.Entities;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class ExperienceViewModel : BaseModel
    {
        public ExperienceViewModel()
        {
            Experience = new EExperience();
            ActionMode = FormActionMode.Add;
        }

        public EExperience Experience { get; set; }
        public List<ELanguage> Languages { get; set; }
    }

    public class ExperienceListViewModel : BaseModel
    {
        public ExperienceListViewModel()
        {
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

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
