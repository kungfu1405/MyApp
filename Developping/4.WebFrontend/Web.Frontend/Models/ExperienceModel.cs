using DbData.Entities;
using Mic.Core.Website;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mic.Core.Entities;
using Mic.UserDb.Entities;

namespace Web.Frontend.Models
{
    public class ExperienceModel : BaseModel
    {
        public ExperienceModel()
        {
            ActionMode = FormActionMode.Add;
        }

        public string MainTitle { get; set;}
        public string MainDescription { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExperienceId { get; set; }
        public string AttractionId { get; set; }
        public List<string> Files { get; set; }

        public EUser Author { get; set; }

        public string Id { get; set; }

        public EExperience Experience { get; set; }
    }
}
