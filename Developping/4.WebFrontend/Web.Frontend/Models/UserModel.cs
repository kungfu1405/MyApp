using DbData.Entities;
using Mic.Core.Entities;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class UserModel
    {
        public UserModel()
        {
            _ListExperience = new ListExperience();
            Pagination = new KTPagination();
        }
        public EUser User;

        public EUserProfile UserProfile;

        //public IList<EExperience> Experiences;
        public ListExperience _ListExperience { get; set; }        
        public KTPagination Pagination { get; set; }
        public KTSort Sort { get; set; }
    }
}
