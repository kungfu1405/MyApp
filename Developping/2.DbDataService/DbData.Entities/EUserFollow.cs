using Mic.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("UserFollow")]
    public class EUserFollow
    {
        public Guid UserId { get; set; }
        public Guid UserFollowingId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Picture { get; set; }

    }
}
