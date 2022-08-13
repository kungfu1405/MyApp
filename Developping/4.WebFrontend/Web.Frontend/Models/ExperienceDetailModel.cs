using DbData.Entities;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class ExperienceDetailModel
    {
        public EExperience Experience;

        public EUser User;

        public EUserProfile UserProfile;

        public IList<EExperience> ListExperienceRelated;

        public String MsgError { get; set; }
    }
}
