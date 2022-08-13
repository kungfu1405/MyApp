using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("Comment_LikeLog")]
    public class EComment_LikeLog
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }

        public int LikeStatus { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual EComment Comment { get; set; }
    }
}
