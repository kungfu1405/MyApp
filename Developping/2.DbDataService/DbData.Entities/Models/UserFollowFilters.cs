using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbData.Entities.Models
{
    public class UserFollowFilters
    {
        public UserFollowFilters()
        {

        }
        public Guid? UserId { get; set; }
        public Guid? FollowingId { get; set; }
        public DatatablePaging Paging { get; set; }
        public DatatableSort Sort { get; set; }
    }
}
