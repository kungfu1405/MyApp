using DbData.Entities;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Core.Website;

namespace Web.Backend.Models
{
    public class ExperienceSessionViewModel : BaseModel
    {
        public ExperienceSessionViewModel()
        {
            ActionMode = FormActionMode.Edit;
        }
        public IList<EExperienceSession> Sessions { get; set; }
        public List<ELanguage> Languages { get; set; }
    }

    public class ExperienceSessionListViewModel : BaseModel
    {
        public ExperienceSessionListViewModel()
        {
        }
        public Guid? ExperienceId { get; set; }

        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
