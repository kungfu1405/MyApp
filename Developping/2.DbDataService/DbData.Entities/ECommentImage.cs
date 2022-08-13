using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbData.Entities
{
    [Table("CommentImage")]
    public class ECommentImage
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }

        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual EComment Comment { get; set; }
    }
}
