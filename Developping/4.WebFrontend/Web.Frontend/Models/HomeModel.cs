using DbData.Entities;
using DbData.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class HomeModel
    {
        public IList<EExperience> ListExperience { get; set; }

        public IList<ItemDiscover> ListItemDiscover { get; set; }

        public IList<ItemConsultant> ListConsultant { get; set; }
    }

    public class ItemDiscover
    {
        public string Tag { get; set; }
        public string Thumbnail { get; set; }

        public EnumPillar Pillar { get; set; }
    }

    public class ItemConsultant
    {
        public string Thumbnail { get; set; }
        public string Name { get; set; }

        public string ObjectName { get; set; }
        public string Description { get; set; }

    }
}
