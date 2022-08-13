using Mic.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Comment")]
    public class EComment : EntityBase<EComment>
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? AttractionId { get; set; }
        public Guid? ExperienceId { get; set; }
        public string Comment { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string DefaultLangCode { get; set; }

        public int StarVoted { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislike { get; set; }
        public int TotalReply { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateByUserName { get; set; }

        [StringLength(50)]
        public string UserAvatarUrl { get; set; }
        public Guid CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid? ModifyByUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int Status { get; set; }

        public virtual EComment Parent { get; set; }
        public virtual EAttraction Attraction { get; set; }
        public virtual EExperience Experience { get; set; }
        public virtual ICollection<ECommentImage> CommentImages { get; set; }
    }
}
