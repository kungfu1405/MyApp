using DbData.Entities;
using DbData.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Frontend.Models
{
    public class PillarModel
    {
        public IList<EExperience> ListExperience { get; set; }

        public EnumPillar Pillar { get; set; }

    }
}
